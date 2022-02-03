<?php


echo '<div class="bigEspace"></div>';
 echo '<main class=colonne>';


echo '<div class="grid-col-9">';

echo '<div> Debut :</div>';
echo '<div></div>';
echo '<div> <input type=text  placeholder="Date"> </div>';
echo '<div></div>';
echo '<div> <input type=text  placeholder="Heure"> </div>';
echo '<div></div>';
echo '<div></div>';
echo '<div></div>';
echo '<div></div>';
echo '<div> Fin :</div>';
echo '<div></div>';
echo '<div> <input type=text  placeholder="Date"> </div>';
echo '<div></div>';
echo '<div> <input type=text  placeholder="Heure"> </div>';
echo '<div></div>';
echo '<div> <input type=button Value="Valider"> </div>';
echo '<div></div>';
echo '<select> 
			<option value="Global">Global</option>
			<option value="Temperatures">Temperatures</option>
			<option value="Sons">Sons</option>
			<option value="lumieres">Lumieres</option>
	  </select>';

echo '</div>';

echo '<div class="bigEspace"></div>';
echo '<div class="graphique center"> Anomalies Statistique Global </div>';
echo '<div></div>';
echo '<div class="BIGEspace"></div>';

$uti =  Afpa_UtilisateursManager::getList(null, ['DateAnomalie' => ]);
echo '<div class="grid-col-3">';
echo '<div class="grid-columns-span-3 caseListe center"> Liste Anomalies Global </div>';
echo '<div class="center caseListe">Type</div>';
echo '<div class="center caseListe">Date</div>';
echo '<div class="center caseListe">Message d\'erreur</div>';
echo '<div class="caseListe">Temperature</div>';
echo '<div class="caseListe"></div>';
echo '<div class="caseListe"></div>';
echo '<div class="caseListe">Lumiere</div>';
echo '<div class="caseListe"></div>';
echo '<div class="caseListe"></div>';


echo '</main>';