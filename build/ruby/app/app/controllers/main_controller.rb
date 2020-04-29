class MainController < ApplicationController
	def index
          @reqHeaders = request.headers.env.select{|key, value| key.in?(ActionDispatch::Http::Headers::CGI_VARIABLES) || key =~ /^HTTP_/}
	end
end
