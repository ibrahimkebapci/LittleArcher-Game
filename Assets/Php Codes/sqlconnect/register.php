<?php
	$con = mysqli_connect('localhost','root','root','unity');
//bağlantı kontrolü için
	if(mysqli_connect_errno())
	{
		echo " 1: Connection failed ";// çalışması durumunda bağlantı başarısız anlamına gelmektedir
		exit();
	}
	$username =$_POST["name"];
	$password =$_POST["password"];
	

	$namecheckquery="SELECT username FROM player WHERE username= '" . $username . "';";// tabloda etkilemek istediğimiz
	$namecheck=mysqli_query($con,$namecheckquery) or die ("2: Name check query failed");//isim kontrolü hatası

	if(mysqli_num_rows($namecheck)>0)
	{
		echo "3: Name alredy exists";// isim kayıt edilemedi hatası
		exit();
	}

	$salt="\$5\$rounds=5000\$" . "steamedhams" . $username . "\$";
	$hash = crypt($password,$salt);
	
	$insertuserquery="INSERT INTO player (username, hash, salt) VALUES ('" . $username . "','" . $hash . "','" . $salt . "');"; //değer yerleştirme

	mysqli_query($con, $insertuserquery) or die("4: Insert player query failed"); 
	
	echo ("0");

?>