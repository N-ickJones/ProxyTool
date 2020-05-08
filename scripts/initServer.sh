#Prereq: Git clone the repo or pull
#Install and prepare all dependencies for ProxyWorld

#Input Variables
  appName=$1

  #Verify an appname was specified
  if [[ -z $appName  ]]; then
    echo "Error: app_name not specified.";
    exit 1;
  else
    echo "appName: $appName";
  fi

#Static Variables
  DIR=/opt/www/ProxyWorld

if [[ $appName == "react" || $appName == "all" ]]; then
  echo "Info: building react client.";
  cd $DIR/build/react/app/ProxyWorldClient;
  npm install;
  npm run build;
  echo "Info: building react server.";
  cd $DIR/build/react/app;
  cp $DIR/scripts/.env/react.settings $DIR/build/react/app/appsettings.json;
  dotnet publish ProxyWorldApi.csproj -c Release;
  echo "Info: finished building react application.";
else
  echo "Info: skipping react.";
fi

if [[ $appName == "razor" || $appName == "all" ]]; then
  echo "Info: building razor application.";
  cd $DIR/build/razor/app
  cp $DIR/scripts/.env/razor.settings $DIR/build/react/app/appsettings.Production.json;
  dotnet publish razor.csproj -c Release
  cd ..
  echo "Info: finished building razor application.";
else
  echo "Info: skipping razor.";
fi

if [[ $appName = "django" || $appName = "all" ]]; then
  echo "Info: building django application.";
  echo "Info: This requires the www:www user and group prerequisite";
  cd $DIR/build
  chown -R www:www django
  cd $DIR/build/django/app;
  python -m venv venv;
  source venv/bin/activate;
  pip install --upgrade pip;
  pip install -r requirements.txt;
  deactivate;
  cd ..;
  echo "Info: finished building django application.";
else
  echo "Info: skipping django.";
fi

if [[ $appName = "flask" || $appName = "all" ]]; then
  echo "Info: building flask application.";
  echo "Info: This requires the www:www user and group prerequisite";
  cd $DIR/build
  chown -R www:www flask;
  cd $DIR/build/flask/app;
  python -m venv venv
  source venv/bin/activate
  pip install --upgrade pip;
  pip install -r requirements.txt;
  deactivate
  cd ..;
  echo "Info: finished building flask application.";
else
  echo "Info: skipping flask.";
fi

if [[ $appName = "ruby" || $appName = "all" ]]; then
  echo "Info: building ruby application.";
  cd $DIR/build/ruby/app;
  bundle install --deployment --without development test
  cd ..;
  echo "Info: finished building ruby application.";
else
  echo "Info: skipping ruby.";
fi

if [[ $appName = "laravel" || $appName = "all" ]]; then
  echo "Info: building laravel application.";
  cd $DIR/build;
  chown -R www:www laravel;
  echo "Info: This requires the www:www user and group prerequisite";
  cd $DIR/build/laravel/app;
  composer install;
  php artisan key:generate;
  php artisan cache:clear;
  php artisan config:clear;
  cd ..;
  echo "Info: finished building laravel application.";
else
  echo "Info: skipping laravel.";
fi

if [[ $appName = "spring" || $appName = "all" ]]; then
  echo "Info: building spring application.";
  cd $DIR/build/spring/app;
  gradle wrapper;
  ./gradle build;  
  cd ..;
  echo "Info: finished building spring application.";
else
  echo "Info: skipping spring.";
fi

if [[ $appName = "phoenix" || $appName = "all" ]]; then
  echo "Info: building phoenix application.";
  cd $DIR/build/phoenix/app;
  mix deps.get;
  mix;
  cd assets;
  npm install;
  cd ..;
  cd ..;
  echo "Info: finished building phoenix application.";
else
  echo "Info: skipping phoenix.";
fi

if [[ $appName = "express" || $appName = "all" ]]; then
  echo "Info: building express application.";
  cd $DIR/build/express/app;
  npm install
  cd ..;
  echo "Info: finished building express application.";
else
  echo "Info: skipping express.";
fi

