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

