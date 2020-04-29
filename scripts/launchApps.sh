#!/usr/bin/env bash

#Input Variables
  appName=$1;

if [[ -z $appName  ]]; then
  firefox -new-tab -url http://react.localhost \
          -new-tab -url http://razor.localhost \
          -new-tab -url http://django.localhost \
          -new-tab -url http://flask.localhost \
          -new-tab -url http://ruby.localhost \
          -new-tab -url http://laravel.localhost \
          -new-tab -url http://spring.localhost \
          -new-tab -url http://phoenix.localhost \
          -new-tab -url http://express.localhost
else
  echo "appName: $appName";
fi

openapps="firefox";

if [[ $appName == "react" ]]; then
  echo "Info: opening react application.";
  firefox -new-tab -url http://react.localhost
fi

if [[ $appName == "razor" ]]; then
  echo "Info: opening razor application.";
  firefox -new-tab -url http://razor.localhost
fi

if [[ $appName = "django" ]]; then
  echo "Info: opening django application.";
  firefox -new-tab -url http://django.localhost
fi

if [[ $appName = "flask" ]]; then
  echo "Info: opening flask application.";
  firefox -new-tab -url http://flask.localhost
fi

if [[ $appName = "ruby" ]]; then
  echo "Info: opening ruby application.";
  firefox -new-tab -url http://ruby.localhost
fi

if [[ $appName = "laravel" ]]; then
  echo "Info: opening laravel application.";
  firefox -new-tab -url http://laravel.localhost
fi

if [[ $appName = "spring" ]]; then
  echo "Info: opening spring application.";
  firefox -new-tab -url http://spring.localhost
fi

if [[ $appName = "phoenix" ]]; then
  echo "Info: opening phoenix application.";
  firefox -new-tab -url http://phoenix.localhost
fi

if [[ $appName = "express" ]]; then
  echo "Info: opening express application.";
  firefox -new-tab -url http://express.localhost
fi


