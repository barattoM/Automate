<?php 
    if ($_SESSION['utilisateur']->getRole() < 2) {
        $displayNone = "noDisplay";
    }else{
        $displayNone = "";
    }
    $dateDuJour = new DateTime('Now');
    $dateDuJour = $dateDuJour->format('Y-m-d')." 00:00:00";
    $ordreDuJour = Afpa_ObjectifsManager::getList(null, ["Date"=>"$dateDuJour"]);
    if ($ordreDuJour == null) {
        $ordreDuJour = new Afpa_Objectifs(["Rendement" => 0,"MaxNombreArretTemperature" => 0,"MaxNombreArretDecibel" => 0,"MaxPourcentDeclasses" => 0]);
    }else{
        $ordreDuJour = $ordreDuJour[0];
    }
?>

<div class="AccueilContenu">
    <div class="marge"></div>

    <div class="AccueilCentre">

        <div class="AccueilPartie1 <?php echo $displayNone ?>">

            <div class="Objectif">
                <h3>Objectif du jour</h3>
                <div class="ObjectifT">
                    <div class="ContenuAccueilG">
                        <div class="contenuObjectif">
                            <div class="Espace04"></div>
                            <div class="txtNom">Rendement : </div>
                            <div class="Espace01"></div>
                            <input type="text" id="inputR" name="inputR" value="<?php echo $ordreDuJour->getRendement() ?>" disabled>
                            <select name="RendementS" class="pourcent">
                                <option value="1">/jour</option>
                                <option value="24">/h</option>
                                <option value="1440">/min</option>
                            </select>
                            <div class="Espace05"></div>
                        </div>
                        <div class="contenuObjectif">
                            <div class="Espace04"></div>
                            <div class="txtNom">Déclassés : </div>
                            <div class="Espace01"></div>
                            <input type="text" id="inputR" name="inputR" value="<?php echo $ordreDuJour->getMaxPourcentDeclasses() ?>" disabled>
                            <div class="Espace01"></div>
                            <div class="pourcent">%</div>
                        </div>
                    </div>

                    <div class="ContenuAccueilG">
                        <fieldset>
                            <legend>Nombre d'arret maximum</legend>
                            <div class="colonne">
                                <div class="contenuObjectif">
                                    <div class="Espace05"></div>
                                    <div class="txtNom">Cause Temperature : </div>
                                    <div class="Espace05"></div>
                                    <input type="text" id="inputR" name="inputR" value="<?php echo $ordreDuJour->getMaxNombreArretTemperature() ?>" disabled>
                            <div class="Espace05"></div>
                                </div>
                                <div class="contenuObjectif">
                                    <div class="Espace05"></div>
                                    <div class="txtNom">Cause sons : </div>
                                    <div class="Espace05"></div>
                                    <input type="text" id="inputR" name="inputR" value="<?php echo $ordreDuJour->getMaxNombreArretDecibel() ?>" disabled>
                            <div class="Espace05"></div>

                                </div>
                            </div>
                        </fieldset>

                    </div>
                </div>
            </div>


            <div class="EspaceObjectifStatus"></div>
            <div class="Status">
                <h3> Status du rendement</h3>
                <div class="barreBasse"></div>
                <img src="IMG/CercleVert.png" alt="">
                <div class="txtNom"> Bon état de rendement</div>
            </div>

        </div>

        <div class="AccueilPartie2 colonne">
            <div>
                <div class="graphique">
                    <div id="graphTemperature" class="graph">
    
                    </div>
                </div>
                <div class="vMini"></div>
                <div class="graphique colonne">
                    <div class="titregraph">
                        Sons statistique
                    </div>
                    <img src="IMG/graph2.jpg" alt="">
                    <div class="infoGraph <?php echo $displayNone ?>">
                        <div>Seuils Minimum</div>
                        <div>2</div>
                        <div>Seuils Maximum</div>
                        <div>50</div>
                    </div>
                </div>
            </div>
            <div>
                <div class="graphique  colonne">
                    <div class="titregraph">
                        Lumieres statistique
                    </div>
                    <img src="IMG/graph3.png" alt="">
                    <div class="infoGraph <?php echo $displayNone ?>">
                        <div>Seuils Minimum</div>
                        <div>2</div>
                        <div>Seuils Maximum</div>
                        <div>50</div>
                    </div>
                </div>
                <div class="vMini"></div>
                <div class="graphique  colonne">
                    <div class="titregraph">
                        Anomalies statistique
                    </div>
                    <img src="IMG/graph4.png" alt="">
                    <div class="infoGraph <?php echo $displayNone ?>">
                        <div>Seuils Minimum</div>
                        <div>2</div>
                        <div>Seuils Maximum</div>
                        <div>50</div>
                    </div>
                </div>
            </div>

        </div>

       
    </div>
     <div class="marge"></div>
</div>