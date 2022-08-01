<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="JuPrinterTest.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">

    <title></title>

    <link href="Bootstrap4/css/reset.css" rel="stylesheet" />
    <link href="Bootstrap4/css/bootstrap.min.css" rel="stylesheet" />    
    <link href="fontawesome/css/all.css" rel="stylesheet" />

    <script type="text/javascript" src="Bootstrap4/js/jquery.min.js"></script>
    <script type="text/javascript" src="Bootstrap4/js/popper.min.js"></script>
    <script type="text/javascript" src="Bootstrap4/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="fontawesome/js/all.min.js"></script>

    <link rel="stylesheet" href="./css/style2.css">
    

</head>

<body>


    <form id="form1" runat="server">
        
         <div class="wrapper">

            <!-- Sidebar  -->
            <nav id="sidebar">

                <div class="sidebar-header">                
                     <h4><i class="fas fa-hospital-user"></i> 老朱柚園訂單管理系統</h4>   
                </div>

                <ul class="list-unstyled components">
                    <li>
                        <a href="./OrderList.aspx" target="iframe" >
                            <h5><i class="fas fa-question-circle" style="width:2rem;"></i> 訂單管理 </h5>                           
                        </a>                       
                    </li>
                    <li>
                        <a href="./CustomerList.aspx" target="iframe" >
                            <h5><i class="fas fa-notes-medical" style="width:2rem;"></i> 客戶資料管理 </h5>             
                        </a>                       
                    </li>
                    <li>
                        <a href="./AddOrder.aspx" target="iframe" >
                            <h5><i class="fas fa-notes-medical" style="width:2rem;"></i> 新增訂單 </h5>             
                        </a>                       
                    </li>

                </ul>


            </nav>

            <!-- Page Content  -->
            <div id="content">
           
                    <div class="container-fluid">
                   	
                        <div style="position:absolute; top: 0%; left:0%; width:100%; height:100%; overflow-x:hidden;overflow-y:hidden;z-index:3;">                 
                            <iframe id="iframe" name="iframe" runat="server" src="" enableviewstate="false" scrolling="yes" style="width: 100%; height: 100%; border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style:none ;" visible="true" frameborder="0">
                            </iframe>  
                        </div>	
 
                    </div>

           </div>


        </div>


    </form>


  </body>

</html>
