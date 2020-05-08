#Input Variables
command=$1

#Verify an appname was specified
if [[ -z $command  ]]; then
  echo "Error: command not specified.";
  exit 1;
else
  echo "command: $command";
fi

systemctl $command react.service
systemctl $command razor.service 
systemctl $command django.service
systemctl $command flask.service
systemctl $command ruby.service
systemctl $command laravel.service
systemctl $command spring.service
systemctl $command phoenix.service
systemctl $command express.service

