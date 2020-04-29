<!DOCTYPE html>
<html lang="en">
    <head>
        <title>Laravel App</title>
    </head>
    <body>
      <div>
	<h1>HTTP Headers</h1>
	
	<?php
		foreach (getallheaders() as $key => $value) {
    			echo "<p><span style='font-weight: bold;'>$key: </span>$value</p>";
		}
	?>
      </div>
    </body>
</html>
