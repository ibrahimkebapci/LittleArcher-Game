<?php

	$con = mysqli_connect('localhost','root','root','unity');
//bağlantı kontrolü için

	if(mysqli_connect_errno())
	{
		echo " 1: Connection failed";// çalışması durumunda bağlantı başarısız anlamına gelmektedir
		exit();
	}

	$username = mysqli_real_escape_string($con,$_POST["name"]);
	$usernameclean = filter_var($username,FILTER_SANITIZE_STRING,FILTER_FLAG_STRIP_LOW | FILTER_FLAG_STRIP_HIGH);
	$password = $_POST["password"];
	
	$namecheckquery = "SELECT username, salt, hash, score FROM player WHERE username='" . $usernameclean . "';";//istenilen değişkenin seçimi
	  
	$namecheck = mysqli_query($con, $namecheckquery) or die ("2: Name check query failed");//isim kontrolü hatası

	if(mysqli_num_rows($namecheck) != 1)
	{
		echo "5: Either no user with name, or more than one";
		exit();
	}
	$existinginfo = mysqli_fetch_assoc($namecheck);
	$salt = $existinginfo["salt"];
	$hash = $existinginfo["hash"];

	$loginhash = crypt($password,$salt);

	if($hash != $loginhash)//parola kontrolü
	{
		echo "6: Incorrect password";
		exit();
	}
	echo "0\t" . $existinginfo["score"];
?>