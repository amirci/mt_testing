require 'rubygems'    
require 'albacore'
require 'rake/clean'
require 'noodle'
  
solution_file = FileList["*.sln"].first
build_file = FileList["*.msbuild"].first

CLEAN.include("main/**/bin", "main/**/obj", "test/**/obj", "test/**/bin")

CLOBBER.include("**/_*", "**/.svn", "lib/*", "**/*.user", "**/*.cache", "**/*.suo")

desc 'Default build'
task :default => ["build:all"]

desc 'Setup requirements to build and deploy'
task :setup => ["setup:dep", "setup:noodle"]

desc 'Build a deploy current version of the framework'
task :deploy => ["deploy:all"]

namespace :setup do

	task :dep do 
		`bundle install `
	end
	
	Noodle::Rake::NoodleTask.new

end

namespace :build do

	desc "Build the project"
	msbuild :all do |msb|
		msb.properties :configuration => :Debug
		msb.targets :Build
		msb.solution = solution_file
	end

	desc "Rebuild the project"
	task :re => ["clean", "build:all"]
end

namespace :test do
	
	desc 'Run all tests'
	msbuild :all do |msb|
		msb.targets :test
		msb.solution = build_file
	end
	
end

namespace :deploy do

	desc 'Updates version, build in release and generate zip on deploy folder'
	task :all  => [:update_version, :default, :package] 
	
	desc 'Updates assembly version'
	assemblyinfo :update_version do |asm|
	  asm.version = "0.2.0.0"
	  asm.company_name = "MavenThought Inc."
	  asm.product_name = "MavenThought Testing Framework"
	  asm.title = "MavenThought Testing"
	  asm.description = "Framework to provide base classes to test enforcing Given, When, Then and using automocking"
	  asm.copyright = "MavenThought Inc. 2010"
	  asm.output_file = "main/MavenThought.Commons.Testing/Properties/AssemblyInfo.cs"
	end	
	
	desc 'Creates a zip with the required assemblies'
	zip :package do |zip|
		zip.directories_to_zip "main/MavenThought.Commons.Testing/bin/release"
		zip.output_file = 'MavenThought.Testing.zip'
		zip.output_path = File.dirname(__FILE__)
	end
	
end

