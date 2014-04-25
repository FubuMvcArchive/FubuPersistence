require 'bundler/setup'
require 'fuburake'

@solution = FubuRake::Solution.new do |sln|
	sln.compile = {
		:solutionfile => 'src/FubuPersistence.sln'
	}

	sln.assembly_info = {
		:product_name => "FubuPersistence",
		:copyright => 'Copyright 2011-2013 Jeremy D. Miller, et al. All rights reserved.'
	}

	sln.ripple_enabled = true
	sln.fubudocs_enabled = true	
	
	sln.options[:nuget_publish_folder] = 'nupkgs'
	sln.options[:nuget_publish_url] = 'https://www.myget.org/F/fubumvc-edge/'


end
