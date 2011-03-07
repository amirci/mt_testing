require 'rubygems'    

puts "Chequing bundled dependencies, please wait...."

system "bundle install --system --quiet"
Gem.clear_paths

require 'albacore'
require 'git'
require 'noodle'
require 'rake/clean'

include FileUtils

solution_file = FileList["*.sln"].first
build_file = FileList["*.msbuild"].first
project_name = "MavenThought.Commons.Testing"
commit = Git.open(".").log.first.sha[0..10] rescue 'na'
version = IO.readlines('VERSION')[0] rescue "0.0.0.0"
deploy_folder = "c:/temp/build/#{project_name}.#{version}_#{commit}"
merged_folder = "#{deploy_folder}/merged"

CLEAN.include("main/**/bin", "main/**/obj", "test/**/obj", "test/**/bin")

CLOBBER.include("**/_*", "**/.svn", "lib/*", "**/*.user", "**/*.cache", "**/*.suo")

desc 'Default build'
task :default => ["build:all"]

desc 'Setup requirements to build and deploy'
task :setup => ["setup:dep"]

desc "Updates build version, generate zip, merged version and the gem in #{deploy_folder}"
task :deploy => ["deploy:all"]

desc "Run all tests"
task :test => ["test:all"]

namespace :setup do
	Noodle::Rake::NoodleTask.new :dep do |n|
		n.groups << :runtime
		n.groups << :dev
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

	task :all  => [:update_version] do
		rm_rf(deploy_folder)
		Dir.mkdir(deploy_folder) unless File.directory? deploy_folder
		Rake::Task["build:all"].invoke(:Release)
		Rake::Task["deploy:package"].invoke
		puts "Please look at #{deploy_folder} for deployed assets"
	end 
	
	task :update_version do 
		files = FileList["main/**/Properties/AssemblyInfo.cs"]
		files.each do |file|
			Rake::Task["deploy:assemblyinfo"].invoke(file) 
			Rake::Task["deploy:assemblyinfo"].reenable 
		end
	end
	
	assemblyinfo :assemblyinfo, :file do |asm, args|
		asm.version = version
		asm.company_name = "MavenThought Inc."
		asm.product_name = "MavenThought Testing Framework"
		asm.title = "MavenThought Testing (sha #{commit})"
		asm.description = "Framework to provide base classes to test enforcing Given, When, Then and using automocking"
		asm.copyright = "MavenThought Inc. 2006 - #{DateTime.now.year}"
		asm.output_file = args[:file]
	end	
		
	task :package do
		[project_name, "#{project_name}.NUnit"].each do | proj |
			puts "Zip contents for #{proj}"
			Rake::Task["deploy:zip"].invoke(proj)
			Rake::Task["deploy:zip"].reenable
		end
	end
		
	zip :zip, :zip_project do |zip, args|
		zip.directories_to_zip "main/#{args[:zip_project]}/bin/release"
		zip.output_file =  "#{args[:zip_project]}.#{version}_#{commit}.zip"
		zip.output_path = deploy_folder
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

end

namespace :jeweler do
	require 'jeweler'  	

	desc 'Build the release version and copy the files to lib'
	task :setup do
		Rake::Task["build:all"].invoke(:Release)
		files = Dir.glob("main/**/bin/release/Maven*.dll")
		copy files, "lib"
	end

	Jeweler::Tasks.new do |gs|
		gs.name = "maventhought.testing"
		gs.summary = "Testing Framework with automocking dependencies"
		gs.description = "Base classes to use for testing (based on MbUnit or Nunit) that enforce Given, When, Then style with auto mocking facilities"
		gs.email = "amir@barylko.com"
		gs.homepage = "http://orthocoders.com"
		gs.authors = ["Amir Barylko"]
		gs.has_rdoc = false  
		gs.rubyforge_project = 'maventhought.testing'  
		gs.files = Dir.glob("lib/Maven*.dll")
	end
end