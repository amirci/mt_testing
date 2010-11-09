require 'rubygems'    
require 'albacore'
require 'rake/clean'
require 'noodle'
require 'zip/zip'
require 'zip/zipfilesystem'
require 'git'
include FileUtils

solution_file = FileList["*.sln"].first
build_file = FileList["*.msbuild"].first
project_name = "MavenThought.Commons.Testing"
commit = Git.open(".").log.first.sha[0..10]
deploy_folder = "c:/temp/build/#{project_name}_#{commit}"
zip_file = "MavenThought.Testing"

CLEAN.include("main/**/bin", "main/**/obj", "test/**/obj", "test/**/bin")

CLOBBER.include("**/_*", "**/.svn", "lib/*", "**/*.user", "**/*.cache", "**/*.suo")

msbuild_path = File.join(ENV['windir'], 'Microsoft.NET','Framework',  'v4.0.30319', 'MSBuild.exe')

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
	msbuild :all do |msb|
		msb.path_to_command =  msbuild_path
		msb.targets :test
		msb.solution = build_file
	end
	
end

namespace :deploy do

	desc 'Updates version, build in release and generate zip on deploy folder'
	task :all  => [:update_version] do
		Rake::Task["build:all"].invoke(:Release)
		Rake::Task["deploy:package"].invoke
		Rake::Task["deploy:merge"].invoke
	end 
	
	task :update_version do 
		files = FileList["main/**/Properties/AssemblyInfo.cs"]
		files.each { |file| Rake::Task["deploy:assemblyinfo"].invoke(file) }
	end
	
	assemblyinfo :assemblyinfo, :file do |asm, args|
		asm.version = "0.2.0.0"
		asm.company_name = "MavenThought Inc."
		asm.product_name = "MavenThought Testing Framework"
		asm.title = "MavenThought Testing (sha #{commit})"
		asm.description = "Framework to provide base classes to test enforcing Given, When, Then and using automocking"
		asm.copyright = "MavenThought Inc. 2010"
		asm.output_file = args[:file]
	end	
		
	desc 'Creates a zip with the required assemblies'
	task :package do
		Dir.mkdir(deploy_folder) unless File.directory? deploy_folder
		curdir = Dir.pwd
		Dir.chdir("main/#{project_name}/bin/release")
		files = FileList["*.dll"]
		puts "Sorry can't find any files in #{Dir.pwd} to add to the zip" unless !files.empty?
		zip_file = "#{deploy_folder}/#{zip_file}_#{commit}.zip"
		puts "Creating zip file #{zip_file}" unless !files.empty?
		Zip::ZipOutputStream.open(zip_file) do |zos|
			files.each do |file|
				# Create a new entry with some arbitrary name
				zos.put_next_entry(file)
				# Add the contents of the file, don't read the stuff linewise if its binary, instead use direct IO
				content = IO.read(file)
				zos.write(content)
			end
		end
		Dir.chdir(curdir)
	end
	
	task :merge do
		puts "Mergin assemblies into one"
		assemblies = FileList["main/#{project_name}/bin/release/*.dll"].join " "
		puts assemblies
		`./tools/ilmerge/ILmerge.exe /out:#{project_name}.dll #{assemblies}`
		merged_folder = "#{deploy_folder}/merged"
		Dir.mkdir(merged_folder) unless File.directory? merged_folder
		mv("#{project_name}.dll", merged_folder)
		rm("#{project_name}.pdb")
	end
	
end

