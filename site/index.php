<?php

if (count($_GET) === 0) {
	$htmlFile = './index.html';

	if (file_exists($htmlFile)) {
			$htmlContent = file_get_contents($htmlFile);
			echo $htmlContent;
	} else echo 'HTML-файл не найден.';
	
} else {
	
	header("Content-Type: application/json");

	function searchEntities($entity, $query) {
		$nextPageUrl = "https://swapi.dev/api/{$entity}/";
	
		$results = [];
	
		while ($nextPageUrl) {
			$response = file_get_contents($nextPageUrl);
			$data = json_decode($response, true);
	
			$entities = array_filter($data['results'], function($e) use ($query) {
				return stripos($e['name'], $query) !== false;
			});
	
			$results = array_merge($results, $entities);
			$nextPageUrl = $data['next'];
		}
	
		return $results;
	}

	$query = $_GET['q'];

	$people = searchEntities('people', $query);
	$planets = searchEntities('planets', $query);
	$ships = searchEntities('starships', $query);

	echo json_encode([
		'people' => array_map(function($el) {
			return ['name' => $el['name'], 'gender' => $el['gender'], 'mass' => $el['mass']];
		}, $people),
		'planets' => array_map(function($el) {
			return ['name' => $el['name'], 'population' => $el['population'], 'diameter' => $el['diameter']];
		}, $planets),
		'ships' => array_map(function($el) {
			return ['name' => $el['name'], 'length' => $el['length'], 'crew' => $el['crew']];
		}, $ships)
	]);

}