<body class="colonne">
    <header>
        <div class="demi"></div>
        
        <div><img src="./IMG/afpa.jfif" alt=""></div>
        <div class="titre"><?php echo $titre; ?></div>
        <div class="colonne">
            <?php

            if (isset($_SESSION['utilisateur'])) {
                echo '<div class="center">'. texte('Bonjour') ." ". $_SESSION['utilisateur']->getNom() . '</div>';
                echo '<div><a href="index.php?page=ActionDeconnexion" class="center">'. texte("Deconnexion") .'</a></div>';
            } else {
                echo '<a href="index.php?page=Default" class="center">'. texte("Connexion") .'</a>';
            }
            ?>

        </div>
        <div class="demi">

        <div id="menu-burger">
	<div class="bar1"></div>
	<div class="bar2"></div>
	<div class="bar3"></div>
</div>
        </div>
    </header>