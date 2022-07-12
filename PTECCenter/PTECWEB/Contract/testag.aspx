<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="testag.aspx.vb" Inherits="PTECCENTER.testag" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Bootstrap Case</title>  
  <meta charset="utf-8"/>  
  <meta name="viewport" content="width=device-width, initial-scale=1"/>  
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css"/>  
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>  
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>  
</head>
<body>
<div class="container">  
  <h2>Dynamic Tabs</h2>  
  <ul class="nav nav-tabs">  
    <li class="active"><a data-toggle="tab" href="#home">Home</a></li>  
    <li><a data-toggle="tab" href="#AboutUs">About Us</a></li>  
    <li><a data-toggle="tab" href="#ContactUs">Contact Us</a></li>  
    <li><a data-toggle="tab" href="#Feedback">Feedback</a></li>  
  </ul>  
  
  <div class="tab-content">  
    <div id="home" class="tab-pane fade in active">  
      <h3>HOME</h3>  
      <p>welcome to Bootstrap Home Page</p>  
    </div>  
    <div id="AboutUs" class="tab-pane fade">  
      <h3>About Us</h3>  
      <p>Welcome to Bootstrap About us page. You can learn more about Bootstrap from W3 School.</p>  
    </div>  
    <div id="ContactUs" class="tab-pane fade">  
      <h3>Contact Us</h3>  
      <p>You can Mail me at:- nilusilu3@gmail.com</p>  
    </div>  
    <div id="Feedback" class="tab-pane fade">  
      <h3>Feedback</h3>  
      <p>Feel Free to comment on my article in CSharpcorner .</p>  
    </div>  
  </div>  
</div>  
  
</body>

</html>
