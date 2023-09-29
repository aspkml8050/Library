// JScript File
    function PrepareMARC_21(ctrl_no,IsMarc21,confirm)
			    {debugger;
			        //var ctrl_no=document.getElementById('hdCtrlNo').value;
			      if(ctrl_no!='' && IsMarc21=='Y' && confirm !="N")
			        {			         
			         CallMarcWebMethod('ctrl_no:'+ctrl_no);
			        }    
			       else
			        {return false;}
			    }
    function UseWebMethod(webURL,methodName,args, onSuccess, onFail){
            $(document).ready(function(){
                    $.ajax({
                            type: "POST",
                            url: webURL + "/" + methodName,
                            data: "{" + args + "}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: onSuccess,
                            error: onFail
                        });
            });
        }
    
        var URL_='';
	    function CallMarcWebMethod(args) 
	        { 
                //var l = arguments.length;
                 //   if (l > 3) 
                  //      {
                  //         for (var i = 3; i < l - 1; i += 2)
                  //              {
                  //               if (args.length != 0) args += ',';
                  //                  args += '"' + arguments[i] + '":"' + arguments[i + 1] + '"';
                   //             }
                   //     }
                var loc = window.location.href;   
                    loc = (loc.substr(loc.length - 1, 1) == "/") ? loc + "SuggService.asmx" : loc;
                var arrUri=loc.split('/'); 
                    URL_=arrUri[0]+'//'+arrUri[2]+'/'+arrUri[3]+'/SuggService.asmx';
                    //*******Pass Parameter's to use Web Method
                  UseWebMethod(URL_,'ImportIntoMARC_21',args,SucceededCallback,Onfailed);
             }

    function SucceededCallback(result)
            {
                // Page element to display feedback.
                if(result==true)
                 {
                    alert("Marc-21 Data is Prepared");
                 } 
                 if(result==false)
                  {
                    alert("Marc-21 Data is not Preparing");
                  }                   
            }
    function Onfailed(result) {//alert("Due system error Marc-21 Data Not Preparing");}
    }