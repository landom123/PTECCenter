<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" %>
<!DOCTYPE html>
<html lang="en">

	<head>
		<title>Asp.net upload example - fileuploader - Innostudio.de</title>
		
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <meta name="description" content="Asp.net upload example - fileuploader - Innostudio.de">
        <meta name="robots" content="noindex">
        
        <link rel="shortcut icon" href="https://innostudio.de/fileuploader/images/favicon.ico">

		<!-- fonts -->
		<link href="https://fonts.googleapis.com/css?family=Roboto:400,700" rel="stylesheet">
		<link href="css/font/font-fileuploader.css" rel="stylesheet">
        
		<!-- styles -->
		<link href="css/jquery.fileuploader.min.css" media="all" rel="stylesheet">
		<link href="css/jquery.fileuploader-theme-dragdrop.css" media="all" rel="stylesheet">
		
		<!-- js -->
		<script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
		<script src="js/jquery.fileuploader.min.js" type="text/javascript"></script>
		<script src="./js/custom.js" type="text/javascript"></script>

		<style>
			body {
				font-family: 'Roboto', sans-serif;
				font-size: 14px;
                line-height: normal;
				background-color: #fff;

				margin: 0;
			}
            
            .fileuploader {
                max-width: 420px;
				margin: 15px;
            }
		</style>
	</head>

	<body>
            <!-- file input -->
			<input type="file" name="files">
    </body>
</html>