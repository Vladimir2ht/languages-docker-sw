<?php

// if (count($_GET) === 0 || count($_GET) === 1) {
if (count($_GET) === 0 || count($_GET) === 1) {
	$html_file = './index.html';

	if (file_exists($html_file)) {
		echo file_get_contents($html_file);
	} else echo 'HTML-файл не найден.';
	
} else {
	
	header("Content-Type: application/json");

	$db_file = './db.json';

	if (file_exists($db_file)) {
		$db_file = json_decode(file_get_contents($db_file));
		
	} else {

		function getEntities($entity) {
			$nextPageUrl = "https://swapi.dev/api/{$entity}/";
		
			$results = [];
		
			$name = 'name';
	
			while ($nextPageUrl) {
				$response = file_get_contents($nextPageUrl);
				$data = json_decode($response, true);
		
				$entities = array_filter($data['results'], function($e) use ($query, $name) {
					return stripos($e[$name], $query) !== false;
				});
		
				$results = array_merge($results, $entities);
				$nextPageUrl = $data['next'];
			}
		
			$results = array_map(function($el) use ($secondField, $thirdField, $name) {
				return [
					$name => $el[$name],
					$secondField => $el[$secondField],
					$thirdField => $el[$thirdField]
				];
			}, $results);
	
			return $results;
		}

		json_encode([
			'people' => searchEntities('people', $query, 'gender', 'mass'),
			'planets' => searchEntities('planets', $query, 'population', 'diameter'),
			'ships' => searchEntities('starships', $query, 'length', 'crew') 
		]);
		
	}

	function searchEntities($entity, $query, $secondField, $thirdField) {
	
		$results = [];
	
		$name = 'name';

		while ($nextPageUrl) {
	
			$entities = array_filter($data['results'], function($e) use ($query, $name) {
				return stripos($e[$name], $query) !== false;
			});

		}
	
		$results = array_map(function($el) use ($secondField, $thirdField, $name) {
			return [
				$name => $el[$name],
				$secondField => $el[$secondField],
				$thirdField => $el[$thirdField]
			];
		}, $results);

		return $results;
	}

	$query = $_GET['q'];

	echo json_encode([
		'people' => searchEntities('people', $query, 'gender', 'mass'),
		'planets' => searchEntities('planets', $query, 'population', 'diameter'),
		'ships' => searchEntities('starships', $query, 'length', 'crew') 
	]);
	
}