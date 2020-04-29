package web.springapp;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.RequestHeader;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.springframework.http.HttpHeaders;

@RestController
public class HomeController {

	@RequestMapping("/")
	public String index(@RequestHeader HttpHeaders headers) {
        String html = "<!DOCTYPE html><html lang='en'><head><title>Spring App</title></head><div>";
	html += "<h1>HTTP Headers</h1>";
		for (Map.Entry<String, List<String>> entry : headers.entrySet()) {
			html +=  "<p><span style='font-weight: bold'>" + entry.getKey() + ":</span> " + entry.getValue() + "</p>";
		}
		html += "</div><html>";
		return html;
	}

}
