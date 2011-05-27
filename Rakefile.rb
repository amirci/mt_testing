require 'rubygems'    

puts "Chequing bundled dependencies, please wait...."

system "bundle install --system --quiet"
Gem.clear_paths

require 'albacore'
require 'git'
require 'rake/clean'

include FileUtils

solution_file = FileList["*.sln"].first
build_file = FileList["*.msbuild"].first
project_name = "MavenThought.Commons.Testing"
commit = Git.open(".").log.first.sha[0..10] rescue 'na'
version = IO.readlines('VERSION')[0] rescue "0.0.0.0"
deploy_folder = "c:/temp/build/#{project_name}.#{version}_#{commit}"
merged_folder = "#{deploy_folder}/merged"

CLEAN.include("main/**/bin", "main/**/obj", "*.xml", "*.gemspec", "*.vsmdi", "test/**/obj", "test/**/bin", "*.testsettings")

CLOBBER.include("**/_*", "**/.svn", "Packages/*", "**/*.user", "**/*.cache", "**/*.suo")

desc 'Default build'
task :default => ["build:all"]

desc 'Setup requirements to build and deploy'
task :setup => ["setup:dep"]

desc "Updates build version, generate zip, merged version and the gem in #{deploy_folder}"
task :deploy => ["deploy:all"]

desc "Run all tests"
task :test => ["test:all"]

namespace :setup do
	desc "Setup dependencies for nuget packages"
	task :dep do
		FileList["**/packages.config"].each do |file|
			sh "nuget install #{file} /OutputDirectory Packages"
		end
	end
end

namespace :build do

	desc "Build the project"
	msbuild :all, [:config] => [:setup] do |msb, args|
		msb.properties :configuration => args[:config] || :Debug
		msb.targets :Build
		msb.solution = solution_file
	end

	desc "Rebuild the project"
	task :re => ["clean", "build:all"]
end

namespace :test do
	
	desc 'Run all tests'
	task :all => [:default] do 
		tests = FileList["test/**/bin/debug/**/*.Tests.dll"].join " "
		system "./tools/gallio/bin/gallio.echo.exe #{tests}"
	end
	
end

namespace :deploy do

	desc "Creates zip files for all the libraries" 
	task :zip_all => ["util:build_release", :zip_task] 
	
	zip :zip_file, :zip_project do |zip, args|
		zip.directories_to_zip "main/#{args.zip_project}/bin/release"
		zip.output_file =  "#{args.zip_project}.#{version}_#{commit}.zip"
		zip.output_path = deploy_folder
	end

	["", ".NUnit", ".xUnit", ".MsTest"].each do | proj |
		zip_task = Rake::Task["deploy:zip_file"]
		task :zip_it do
			zip_file = "#{project_name}proj"
			puts "Zip contents for #{zip_file}"
			zip_task.invoke(zip_file)
			zip_task.reenable
		end
	end

	
	task :merge do
		puts "Merging #{project_name} assemblies located in bin/release into one"
		assemblies = FileList["main/#{project_name}/bin/release/*.dll"]
		assemblies = assemblies.sort { |f1, f2| f1.include?( "Testing.dll" ) ? -1 : 0 } .join " "
		system "./tools/ilmerge/ILmerge.exe /out:#{project_name}.dll #{assemblies}"
		Dir.mkdir(merged_folder) unless File.directory? merged_folder
		mv("#{project_name}.dll", merged_folder)
		rm("#{project_name}.pdb")
	end

	desc "Publish nuspec package"
	task :publish  => ["util:build_release"] do
		nuget_lib = "nuget/lib"
		clean_folder = Rake::Task["util:clean_folder"]
		package = Rake::Task["deploy:package"]
		["", "nunit", "mstest", "xunit"].each do |ext|
			clean_folder.invoke("nuget")
			mkdir nuget_lib
			cp FileList["main/**/bin/release/MavenT*#{ext}.dll"][0], nuget_lib
			nuget_package = "maventhought.testing#{ext.empty? ? "" : "." + ext}"
			package.invoke(nuget_package)
			clean_folder.reenable
			package.reenable
			sh "nuget push nuget/#{nuget_package}.#{version}.nupkg" 
		end
	end 

	nuspec :spec, :package_id  do |nuspec, args|
	   nuspec.id = args.package_id
	   nuspec.version = version
	   nuspec.authors = "Amir Barylko"
	   nuspec.owners = "Amir Barylko"
	   nuspec.description = "Framework to provide base classes to test enforcing Given, When, Then and using automocking"
	   nuspec.summary = "Framework to provide base classes to test enforcing Given, When, Then and using automocking"
	   nuspec.language = "en-US"
	   nuspec.licenseUrl = "https://github.com/amirci/mt_testing/LICENSE"
	   nuspec.projectUrl = "https://github.com/amirci/mt_testing"
	   nuspec.working_directory = "nuget"
	   nuspec.output_file = "#{args.package_id}.#{version}.nuspec"
	   nuspec.tags = "testing automocking givenwhenthen"
	   nuspec.dependency "CommonServiceLocator", "1.0"
	   nuspec.dependency "RhinoMocks", "3.6"
	   nuspec.dependency "structuremap.automocking", "2.6.2"
	   nuspec.dependency "nunit", "[2.5.9]" if args.package_id.include? "nunit"
	   nuspec.dependency "xunit", "1.7.0" if args.package_id.include? "xunit"
	   nuspec.dependency "gallio", "3.2.601" unless args.package_id =~ /nunit|xunit|mstest/
	end
	
	nugetpack :package, :package_id do |p, args|
		spec = Rake::Task["deploy:spec"]
		spec.invoke(args.package_id)
		spec.reenable
		p.nuspec = "nuget/#{args.package_id}.#{version}.nuspec"
		p.output = "nuget"
	end
end

namespace :util do
	task :clean_folder, :folder do |t, args|
		rm_rf(args.folder)
		Dir.mkdir(args.folder) unless File.directory? args.folder
	end
	
	assemblyinfo :update_version, :file do |asm, args|
		asm.version = version
		asm.company_name = "MavenThought Inc."
		asm.product_name = "MavenThought Testing (sha #{commit})"
		asm.copyright = "MavenThought Inc. 2006 - #{DateTime.now.year}"
		asm.output_file = "GlobalAssemblyInfo.cs"
	end	

	task :build_release => [:update_version] do 
		Rake::Task["build:all"].invoke(:Release)
	end
end