<?php

class Afpa_Seuils 
{

	/*****************Attributs***************** */

	private $_IdSeuil;
	private $_SeuilBas;
	private $_SeuilHaut;
	private $_DateSeuil;
	private $_Temps;
	private static $_attributes=["IdSeuil","SeuilBas","SeuilHaut","DateSeuil","Temps"];
	/***************** Accesseurs ***************** */


	public function getIdSeuil()
	{
		return $this->_IdSeuil;
	}

	public function setIdSeuil(int $IdSeuil)
	{
		$this->_IdSeuil=$IdSeuil;
	}

	public function getSeuilBas()
	{
		return $this->_SeuilBas;
	}

	public function setSeuilBas(?int $SeuilBas)
	{
		$this->_SeuilBas=$SeuilBas;
	}

	public function getSeuilHaut()
	{
		return $this->_SeuilHaut;
	}

	public function setSeuilHaut(?int $SeuilHaut)
	{
		$this->_SeuilHaut=$SeuilHaut;
	}

	public function getDateSeuil()
	{
		return is_null($this->_DateSeuil)?null:$this->_DateSeuil->format('Y-n-j');
	}

	public function setDateSeuil(?string $DateSeuil)
	{
		$this->_DateSeuil=is_null($DateSeuil)?null:DateTime::createFromFormat("Y-n-j",$DateSeuil);
	}

	public function getTemps()
	{
		return $this->_Temps;
	}

	public function setTemps(?int $Temps)
	{
		$this->_Temps=$Temps;
	}

	public static function getAttributes()
	{
		return self::$_attributes;
	}

	/*****************Constructeur***************** */

	public function __construct(array $options = [])
	{
 		if (!empty($options)) // empty : renvoi vrai si le tableau est vide
		{
			$this->hydrate($options);
		}
	}
	public function hydrate($data)
	{
 		foreach ($data as $key => $value)
		{
 			$methode = "set".ucfirst($key); //ucfirst met la 1ere lettre en majuscule
			if (is_callable(([$this, $methode]))) // is_callable verifie que la methode existe
			{
				$this->$methode($value===""?null:$value);
			}
		}
	}

	/*****************Autres Méthodes***************** */

	/**
	* Transforme l'objet en chaine de caractères
	*
	* @return String
	*/
	public function toString()
	{
		return "IdSeuil : ".$this->getIdSeuil()."SeuilBas : ".$this->getSeuilBas()."SeuilHaut : ".$this->getSeuilHaut()."DateSeuil : ".$this->getDateSeuil()."Temps : ".$this->getTemps()."\n";
	}
}