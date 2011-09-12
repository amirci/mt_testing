MavenThought Testing framework
Copyright MavenThought Inc 2010-2011.

In order to compile:

* Install ruby (or iron ruby) and add it to the path
	- u may need also to do: gem install rake bundler
* Install nuget and add it to the path
* Run: rake (should build)
* Run: rake test (should run all the tests)

Log:

0.3.7
- Updated projects that didnt use nuget packages
- Updated structuremap to 2.6.3

0.3.6
- Merged pull request #2 from TheEvilDev/master

0.3.5
- Updated dependency with nunit 

0.3.4
- Fixed issue with MsTest, was using the wrong setup attribute