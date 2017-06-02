App.service('util_SERVICE', ['$http', '$window', '$cookieStore', '$rootScope', function ($http, $window, $cookie, $rootScope) {
    var urlsd = window.location.href.split("/");
    //this.url = "http://192.168.0.38:82/DOService.asmx/";
	//this.Burl = "http://192.168.0.38:82";
	this.url = "http://192.168.0.38:87/TEAssignment.asmx/";
	this.Burl = "http://54.251.51.69:3890";
	
    this.config = {
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
        }
    }
	
	this.islogin = function()
	{
		if($cookie.get('Islogin')==false || $cookie.get('Islogin')===undefined)
		{
			window.location = "login.html";
		}
	}
	
	
	
	
	
	
	//Get DOA_Search Other DO and CN
    this.DOA_SearchOtherDOandCN = function (d1,d2,company,cname,docnumber,DIN,status,type) {
        var parms = {"sFromDate" : d1,"sToDate" : d2,"sCustName":cname,"sDocNum":docnumber,"sDriverIncharge":DIN,"sStatus":status,"sType":type};
        var promise = $http.post(this.url + "DOA_SearchOtherDOandCN", "sJsonInput=" + JSON.stringify(parms)+"&"+"sCompany="+company, this.config)
   .success(function (response) {
       if (response.returnStatus == 1) {
           return response;
       } else {
           //alert('Not Connecting to server');
           return false;
       }
   });
        return promise;

    };
	
	
	
    this.systemId = "4";
    this.authKey = "adfs3sdaczxcsdfw34";
    this.userId = "4";
    this.roleName = "4";
    this.grequestId = function () {
        return "1111";
    }

    this.msg;
    this.alerts = [];
    this.catstatus = function () {
        return $cookie.get('catstatus');
    }

    this.setmsg = function (d) {
        this.msg = d;
    }



    this.addAlert = function (type, msgs) {
        var length = this.alerts.push({ "type": type, "msg": msgs });
        //$rootScope.fadAlert(length-1);

        //console.log(this.alerts);
        //$rootScope.$broadcast("hi");

    };






    this.errorsomething = function (d) {
        //switch (d.error.code) {
        //    case 16: document.write("Not Saturday"); break;
        //    case 20: document.write("Not Sunday"); break;
        //    default: this.addAlert("danger", d.error.Message); break;
        //}
        if (d.error.code > 0)
        // alertify.alert(d.error.detatiledMessage);
            this.addAlert("danger", d.error.Message);

    }

    //eh = error handling
    this.eh = function (d) {
        /*console.log(d);
        if (d.returnStatus == 1 || d.error == null || d.error.code == 0) {
        return true;
        }
        else
        this.errorsomething(d);*/

        if (d.code > 0)
            this.addAlert("danger", d.message);
    }
    this.getserverURL = function () {
        return url;
    };

    this.gsid = function () {
        return $cookie.get('sessionId');
    };

   

        this.checkmenu = function (id) {
            if ($cookie.get('usermenu') == "" || $cookie.get('usermenu') == undefined)
                return false;

            //console.log($cookie.get('usermenu'));

            var key = JSON.parse($cookie.get('usermenu'));
            var i = null;
            var ret = false;
            for (i = 0; key.length > i; i += 1) {
                if (key[i].menuId == id) {
                    ret = true;
                }
            };
            return ret;
        };

       

        this.isUndefinedOrNull = function (val) {
            return angular.isUndefined(val) || val === null
        }






    } ]);


//fullscreen btn 
 function maxWindow() {
        window.moveTo(0, 0);


        if (document.all) {
            top.window.resizeTo(screen.availWidth, screen.availHeight);
        }

        else if (document.layers || document.getElementById) {
            if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {
                top.window.outerHeight = screen.availHeight;
                top.window.outerWidth = screen.availWidth;
            }
        }
    }