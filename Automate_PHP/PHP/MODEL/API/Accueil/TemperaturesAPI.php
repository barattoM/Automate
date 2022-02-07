<?php
$date = $_POST['DateNow'];
$dateInterval = "'".$date." 00:00:00"."'->'".$date." 23:59:59'";
echo json_encode(Afpa_TemperaturesManager::getList(null, ['DateTemperature' => $dateInterval],null, null, true));
?>