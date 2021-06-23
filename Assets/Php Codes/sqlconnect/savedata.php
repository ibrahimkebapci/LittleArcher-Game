<?php
$con = mysqli_connect('localhost','root','root','unity');
//bağlantı kontrolü için

	if(mysqli_connect_errno())
	{
		echo " 1: Connection failed"; // çalışması durumunda bağlantı başarısız anlamına gelmektedir
		exit();
	}

	$username=$_POST["name"];
	$newscore=$_POST["score"];

	//double check there is only user with this name 

	$namecheckquery = "SELECT username FROM player WHERE username='" . $username . "';";//istenilen değişkenin seçimi

	$namecheck=mysqli_query($con,$namecheckquery) or die("2: Name check query failed");
	if(mysqli_num_rows($namecheck) != 1)
	{
		echo "5: Either no user with name, or more than one";
		exit();
	}
	$updatequery="UPDATE player SET score = ".$newscore . " WHERE username = '".$username."';";// değer güncelleme işlemleri 
	mysqli_query($con,$updatequery)or die ("7 : save query failed");

	echo "0";

?>