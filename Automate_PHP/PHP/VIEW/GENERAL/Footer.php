</div>
<div class ="bigEspace"></div>
<div class ="bigEspace"></div>
<footer Class="background ">
<div class= "colonne center Alan"> 
    <div>AFPA</div>
    <div>Adresse : 407 Av. de la Gironde, 59640 Dunkerque</div>
    <div>Réalisé par la session CDA 2021/2022</div>
    </div>
</footer>
<?php
 if (substr($page[1],0,4)=="Form"){
    echo ' <script src="./JS/VerifForm.js"></script>';
    if ($page[1] == "FormAfpa_Seuils") {
        echo ' <script src="./JS/Seuil/uniteMesureSeuil.js"></script>';
    }
 }else if($page[1] == "Accueil" && $_SESSION['utilisateur']->getRole() > 1){
    echo '<script src="./JS/Accueil/SelectRendement.js"></script>';
 }
 echo ' <script src="./JS/script.js"></script>';
echo '</body>
</html>';