<?php

if (count($_GET) === 0) {
	$htmlFile = './index.html';

	if (file_exists($htmlFile)) {
			$htmlContent = file_get_contents($htmlFile);
			echo $htmlContent;
	} else echo 'HTML-файл не найден.';
	
} else {
	echo implode($_GET);
}

// echo implode($_GET);
