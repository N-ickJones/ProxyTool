

var express = require('express');
var app = express();

app.get('/', function (req, res) {
	let html = "<!DOCTYPE html><html lang='en'><head><title>Express App</title></head><div>";
	html += "<h1>HTTP Headers</h1>";
	for (header in req.headers) {
	  html += "<p><span style='font-weight: bold;'>" + header + ": </span>" + req.headers[header] + "</p>"
	};
	html += "</div><html>";
  	res.send(html);
});

var server = app.listen(8008, function () {
  console.log('Express server running on port 8008');
});


process.on('SIGINT', function() {
    server.close(function() {
      console.log("\nExpress server has been shutdown");
    });
});


