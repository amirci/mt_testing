require 'rubygems'    

require 'albacore'
require 'rake/clean'
require 'zip/zip'
require 'zip/zipfilesystem'
require 'git'
require 'rake/gempackagetask'
require 'noodle'

include FileUtils

solution_file = FileList["*.sln"].first
build_file = FileList["*.msbuild"].first
project_name = "MavenThought.Commons.Testing"
commit = Git.open(".").log.first.sha[0..10]
version = "0.2.0.0"
deploy_folder = "c:/temp/build/#{project_name}.#{version}_#{commit}"
merged_folder = "#{deploy_folder}/merged"

CLEAN.include("main/**/bin", "main/**/obj", "test/**/obj", "test/**/bin")

CLOBBER.include("**/_*", "**/.svn", "lib/*", "**/*.user", "**/*.cache", "**/*.suo")

msbuild_path = File.join(ENV['windir'], 'Microsoft.NET','Framework',  'v4.0.30319', 'MSBuild.exe')

desc 'Default build'
task :default => ["build:all"]

desc 'Setup requirements to build and deploy'
task :setup => ["setup:dep:download", "setup:dep:local"]

desc "Updates build version, generate zip, merged version and the gem in #{deploy_folder}"
task :deploy => ["deploy:all"]

desc "Run all tests"
task :test => ["test:all"]

namespace :setup do
	namespace :dep do
		task :download do 
			system "bundle install --system"
		end	
		Noodle::Rake::NoodleTask.new :local do |n|
			n.groups << :dev
		end
	end
end

namespace :build do

	desc "Build the project"
	msbuild :all, :config do |msb, args|
		msb.path_to_command =  msbuild_path
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
		Rake::Task["deploy:merge"].invoke
		Rake::Task["deploy:gem"].invoke
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
		asm.copyright = "MavenThought Inc. 2010"
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

	task :gem do
		rm_rf('gem/lib') if File.directory?('gem/lib')
		mkdir('gem/lib')
		FileList["main/#{project_name}/bin/release/*.dll"].each { |f| cp(f, "gem/lib") }
		chdir('gem')
		spec = eval(IO.read("maventhought.testing.gemspec"))
		spec.version = version
		Gem::Builder.new(spec).build
		chdir('..')
		FileList["gem/maventhought.testing-*.gem"].each { |f| mv(f, deploy_folder) }
	end
  
end