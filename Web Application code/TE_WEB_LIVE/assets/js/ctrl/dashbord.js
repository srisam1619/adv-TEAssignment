App.controller('dashbord_ctrl', ['$scope', '$rootScope', '$http', '$window', '$cookies','util_SERVICE',

function ($scope, $rootScope, $http, $window, $cookies, US) {

  	US.islogin();
	$scope.CalenderViewData = [];
	$scope.sDate = "";
	
   
 $scope.userdata = JSON.parse($cookies.get('UserData'));
 $scope.calltypeSelected = "";
 
 	$scope.closemenu = function()
{
	if($scope.bodyclass=='closed-sidebar')
		$scope.bodyclass="";
	else
		$scope.bodyclass="closed-sidebar";
}
$scope.closemenu();

$scope.openAAmodal = function(id)
{
	$rootScope.currentRowID = id;
	$scope.GetAAEngineerList();
}
$rootScope.setAAengg = function(ename,eid,status)
{
	if(status=="Available")
	{
		for(var i=0;i<$scope.ServiceCall.length;i++)
		{
			if($scope.ServiceCall[i].RowNumber==$rootScope.currentRowID)
			{
				$scope.ServiceCall[i].AssignedEngineer=ename;
				$scope.ServiceCall[i].AssignedEngineerId=eid;
				$('#AAModal').modal('hide');
				
			}
			
		}
	}
	else
	{
		alert("Engineer not available to select");
	}
}

$scope.getLength = function(obj) {
    return Object.keys(obj).length;
}

$scope.SubmitAssEngg = function()
{

        var config = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
            }
        }
		$scope.ServiceCallSaveData = [];

    	 for(var i = 0;i<$scope.ServiceCall.length;i++) 
		{
			
			if($scope.ServiceCall[i].AssignedEngineer===undefined)
			{
				console.log($scope.ServiceCall[i].AssignedEngineer);
			}
			else
			{
				$scope.ServiceCallSaveData.push($scope.ServiceCall[i]);
			}
		}
	 
	 
	 
	 if($scope.ServiceCallSaveData.length==0)
	 {
		 alert("Assign Engineer to any one record");
	 }
	 else
	 {
        $http.post(US.url+"SubmitTEAssignment", "sJsonInput=" + encodeURIComponent(JSON.stringify($scope.ServiceCallSaveData))+"&sCompany="+$scope.userdata[0].CompanyCode, config)
   .then(
       function (response) {
           // success callback
		   if(response.data[0].Result=="Success")
		   {
			  console.log(response.data);
			  alert(response.data[0].DisplayMessage);
		   }
		   else
		   {
			   alert(response.data[0].DisplayMessage);
		   }
           
       },
       function (response) {
           // failure callback

       }
    );

	 }
}

 console.log($scope.userdata);
 $scope.GetCallType = function () {

        var data = {"sCompany" : $scope.userdata[0].CompanyCode}

        var config = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
            }
        }

        var parms = JSON.stringify(data);
        $http.post(US.url+"GetCallType", "sJsonInput=" + parms, config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
          $scope.calltype = response.data;
           
       },
       function (response) {
           // failure callback

       }
    );

    }
	
	
	
	$scope.GetServiceCall = function () {

        var data = {"sCompany" : $scope.userdata[0].CompanyCode}

        var config = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
            }
        }

        var parms = JSON.stringify(data);
        $http.post(US.url+"GetServiceCall", "sJsonInput=" + parms, config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
          $scope.ServiceCall = response.data;
		  
           
       },
       function (response) {
           // failure callback

       }
    );

    }

$scope.ShowCalender = function () {
		
			if($scope.sDate===undefined || $scope.sDate=="" )
		{
			alert("Kindly Select Date.");
			return;
		}
		var config = {headers: {'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'}}
		//var parms = JSON.stringify(data);
        $http.post(US.url+"GetEngineerCalenderView", "sCompany=" + $scope.userdata[0].CompanyCode+"&sDate="+$scope.sDate, config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
          $scope.CalenderView = response.data;
		  if( $scope.CalenderView.length==0)
		  {
			  alert("No data found.");
		  }
		  else
		  {
		 $scope.groupNow($scope.CalenderView);
		  }
           
       },
       function (response) {
           // failure callback

       }
    );

    }
	
	
	
	$scope.GetEngineerList = function () {
		
			if($scope.sDate===undefined || $scope.sDate=="" )
		{
			alert("Kindly Select Date.");
			return;
		}

        var data = {"sCompany" : $scope.userdata[0].CompanyCode,"sDate":$scope.sDate}

        var config = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
            }
        }

        var parms = JSON.stringify(data);
        $http.post(US.url+"GetEngineerList", "sJsonInput=" + parms, config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
          $rootScope.EngineerList = response.data;
		  $('#CCModal').modal('show');
           
       },
       function (response) {
           // failure callback

       }
    );

    }
	
	
	
	$scope.SaveEnggList = function () {
		
			

        //var data = {"sCompany" : ,"sDate":$scope.sDate}

        var config = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
            }
        }

        var parms = JSON.stringify( $rootScope.EngineerList, function( key, value ) {
    if( key === "$$hashKey" ) {
        return undefined;
    }

    return value;
});
        $http.post(US.url+"SaveEngineerAvailabilitySetUp", "sJsonInput=" + parms+"&sCompany="+$scope.userdata[0].CompanyCode, config)
   .then(
       function (response) {
           // success callback
		   if(response.data[0].Result=="Success")
		   {
			  console.log(response.data);
			  $rootScope.EngineerList = response.data;
			  $('#CCModal').modal('hide');
			  alert(response.data[0].DisplayMessage);
		   }
		   else
		   {
			   alert(response.data[0].DisplayMessage);
		   }
           
       },
       function (response) {
           // failure callback

       }
    );

    }
	
	
	$scope.GetAAEngineerList = function () {
		
	if($scope.sDate=="")
	{
		alert("Select Date");
	}
	else
	{
	

        var data = {"sCompany" : $scope.userdata[0].CompanyCode,"sDate":$scope.sDate}

        var config = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
            }
        }

        var parms = JSON.stringify(data);
        $http.post(US.url+"GetAssignedEngineer", "sJsonInput=" + parms, config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
          $rootScope.AAEngineerList = response.data;
		  $('#AAModal').modal('show');
           
       },
       function (response) {
           // failure callback

       }
    );
   
	}

    }
	$scope.gettime = function(t)
	{
		if(t.length>=4)
		{
			return parseInt(t.substring(0, 2))
		}
		else
		{
			return parseInt(t.substring(0, 1))
		}
	}
	
	$scope.setBG= function()
	{
		for(var i = 0;i<$scope.CalenderViewData.length;i++) 
		{
			var bigone = 0;
			var bigKey = "";
			for (key in $scope.CalenderViewData[i].TimeData[0]){
        		if(parseInt(bigone)<=parseInt($scope.CalenderViewData[i].TimeData[0][key].EndTime))
				{
				   console.log( key + ": " + $scope.CalenderViewData[i].TimeData[0][key].EndTime);
				   bigone = $scope.CalenderViewData[i].TimeData[0][key].EndTime;
				   bigKey = key;
				   if($scope.CalenderViewData[i].TimeData[0][key].EndTime!="")
				   {
				   		$scope.CalenderViewData[i].TimeData[0][key].LR="white";
				   }
				    if($scope.CalenderViewData[i].TimeData[0][bigKey].Result=="" && ($scope.CalenderViewData[i].TimeData[0][bigKey].EndTime=="" ||$scope.CalenderViewData[i].TimeData[0][bigKey].EndTime=="0") )
				   {
					   $scope.CalenderViewData[i].TimeData[0][key].LR="Maroon";
				   }
				   if($scope.CalenderViewData[i].TimeData[0][bigKey].Status=="Unavailable to take service calls")
				   {
					   $scope.CalenderViewData[i].TimeData[0][key].LR="Gray";
				   }
				   
				   	
				}
				
    		}
			if($scope.CalenderViewData[i].TimeData[0][bigKey].Result=="Completed")
			{
				$scope.CalenderViewData[i].TimeData[0][bigKey].LR="green";
			}
			else if($scope.CalenderViewData[i].TimeData[0][bigKey].Result=="")
			{
				$scope.CalenderViewData[i].TimeData[0][bigKey].LR="";
			}
			else
			{
				$scope.CalenderViewData[i].TimeData[0][bigKey].LR="none";
			}
			
		}
	}
	
	$scope.groupNow = function()
	{
		$scope.CalenderViewData = [];
		for(var i = 0;i<$scope.CalenderView.length;i++)
		{
			
				var time = $scope.gettime($scope.CalenderView[i].RespTime);
				var timedata = "ss";timedataval="";
				var index = $scope.CalenderViewData.findIndex(x => x.EmployeeName==$scope.CalenderView[i].EmployeeName)
				if(time>=8 && time<9)
					{timedata="AAM";timedataval=$scope.CalenderView[i];}
				else if(time>=9 && time<10)
					{timedata="BAM";timedataval=$scope.CalenderView[i];}
				else if(time>=10 && time<11)
					{timedata="CAM";timedataval=$scope.CalenderView[i];}
				else if(time>=11 && time<12)
					{timedata="DAM";timedataval=$scope.CalenderView[i];}
				else if(time>=12 && time<13)
					{timedata="EPM";timedataval=$scope.CalenderView[i];}
				else if(time>=13 && time<14)
					{timedata="FPM";timedataval=$scope.CalenderView[i];}
				else if(time>=14 && time<15)
					{timedata="GPM";timedataval=$scope.CalenderView[i];}
				else if(time>=15 && time<16)
					{timedata="HPM";timedataval=$scope.CalenderView[i];}
				else if(time>=16 && time<17)
					{timedata="IPM";timedataval=$scope.CalenderView[i];}
				else if(time>=17 && time<18)
					{timedata="JPM";timedataval=$scope.CalenderView[i];}
				else if(time>=18 && time<19)
					{timedata="KPM";timedataval=$scope.CalenderView[i];}
				else if(time>=19 && time<20)
					{timedata="LPM";timedataval=$scope.CalenderView[i];}
				else
					{timedata="ss";timedataval=$scope.CalenderView[i];}
					
				if (index === -1){
					$scope.CalenderViewData.push({"EmployeeName":$scope.CalenderView[i].EmployeeName,"TimeData":[{}]});
					var indexAgain = $scope.CalenderViewData.findIndex(x => x.EmployeeName==$scope.CalenderView[i].EmployeeName)
					$scope.CalenderViewData[indexAgain].TimeData[0][timedata] = timedataval ;
					
				}
				else
				{
					$scope.CalenderViewData[index].TimeData[0][timedata] = timedataval ;
					//console.log($scope.CalenderViewData[index].EmployeeName);
				}
				
			
		}
		//JSON.parse($scope.CalenderViewData);
		//alert($scope.CalenderViewData[0].TimeData[0]);
		$scope.setBG();
		
	}
	
	
$scope.GetCallType();
$scope.GetServiceCall();
//$scope.GetEngineerList();

} ]);
