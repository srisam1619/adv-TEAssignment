App.controller('customerContract_ctrl', ['$scope', '$rootScope', '$http', '$window', '$cookies',

function ($scope, $rootScope, $http, $window, $cookies) {
    $scope.savelable = "Save";
    $scope.savebtn = false;
    $scope.data = {
    "CCON": [
        {
            "U_Conno": "1103",
            "U_Status": "OPEN",
            "U_Uname": "Bathra",
            "U_Cdate": "20-03-2016",
            "U_Ccode": "0005",
            "U_Cname": "RAM",
            "U_CPeriod1": "20-03-2016",
            "U_CPeriod2": "20-03-2018",
            "U_Pcode": "RRD1",
            "U_AddrN": "ajithnagar",
            "U_Addr1": "annanagar",
            "U_Addr2": "annanagar",
            "U_Addr3": "annanagar",
            "U_Addr4": "annanagar",
            "U_Addr5": "annanagar",
            "U_Addr6": "annanagar",
            "U_TelNo": "5210422",
            "U_FaxNo": "52120422",
            "U_Mno": "9629476950",
            "U_Email": "madeswaran1986@gmail.com",
            "U_Remarks": "madeswaran1986@gmail.com"
        }
    ],
    "CCONGEN": [
        {
            "U_Stype": "General",
            "U_Conti": "56",
            "U_Country": "India",
            "U_City": "namakkal",
            "U_Currency": "INR",
            "U_EQGroup": "medical",
            "U_Rate": "56",
            "U_UOM": "UNIT",
            "U_GST": "GST 2 %",
            "U_Remarks": "Testing data values"
        },
        {
            "U_Stype": "Sub",
            "U_Conti": "20",
            "U_Country": "Signapore",
            "U_City": "namakkal",
            "U_Currency": "INR",
            "U_EQGroup": "medical",
            "U_Rate": "56",
            "U_UOM": "UNIT",
            "U_GST": "GST 4 %",
            "U_Remarks": "Testing data values2"
        },
        {
            "U_Stype": "Private",
            "U_Conti": "26",
            "U_Country": "China",
            "U_City": "namakkal",
            "U_Currency": "INR",
            "U_EQGroup": "medical",
            "U_Rate": "56",
            "U_UOM": "UNIT",
            "U_GST": "GST 5 %",
            "U_Remarks": "Testing data values3"
        }
    ],
    "CCONADD": [
        {
            "U_Ctype": "General",
            "U_Continent": "56",
            "U_Country": "India",
            "U_City": "namakkal",
            "U_Currency": "INR",
            "U_EQGroup": "medical",
            "U_Rate": "56",
            "U_UOM": "UNIT",
            "U_GST": "GST 2 %",
            "U_Remarks": "Testing data values"
        },
        {
            "U_Ctype": "Sub",
            "U_Continent": "56",
            "U_Country": "India",
            "U_City": "namakkal",
            "U_Currency": "INR",
            "U_EQGroup": "medical",
            "U_Rate": "56",
            "U_UOM": "UNIT",
            "U_GST": "GST 2 %",
            "U_Remarks": "Testing data values"
        },
        {
            "U_Ctype": "Private",
            "U_Continent": "56",
            "U_Country": "India",
            "U_City": "namakkal",
            "U_Currency": "INR",
            "U_EQGroup": "medical",
            "U_Rate": "56",
            "U_UOM": "UNIT",
            "U_GST": "GST 2 %",
            "U_Remarks": "Testing data values"
        }
    ]
}

    $scope.g_add = function () {

        $scope.gen = {
            "U_Stype": $scope.g_Stype,
            "U_Conti": $scope.g_Continent,
            "U_Country": $scope.g_Country,
            "U_City": $scope.g_city,
            "U_Currency": $scope.g_curency,
            "U_EQGroup": $scope.g_egroup,
            "U_Rate": $scope.g_rate,
            "U_UOM": $scope.g_uom,
            "U_GST": $scope.g_gst,
            "U_Remarks": $scope.g_remark,
        };
        $scope.data.CCONGEN.push($scope.gen);
        $scope.g_reset();
    }

    $scope.g_reset = function () {

            $scope.g_Stype = "";
            $scope.g_Continent= "";
            $scope.g_Country= "";
            $scope.g_city= "";
            $scope.g_curency= "";
            $scope.g_egroup= "";
            $scope.g_rate= "";
            $scope.g_uom= "";
            $scope.g_gst= "";
            $scope.g_remark= "";
        
        
    }


    $scope.g_edit = function(i)
    {
       // alert(i);
        $scope.data.CCONGEN[i].edit = true;
    }
    $scope.g_ok = function(i)
    {
       // alert(i);
        $scope.data.CCONGEN[i].edit = false;
    }

    $scope.g_del = function(i)
    {
        //alert(i);
        $scope.data.CCONGEN.splice(i, 1); 
    }
     $scope.g_copy = function(i)
    {  
            $scope.g_Stype = $scope.data.CCONGEN[i].U_Stype;
            $scope.g_Continent= $scope.data.CCONGEN[i].U_Conti;
            $scope.g_Country= $scope.data.CCONGEN[i].U_Country;
            $scope.g_city= $scope.data.CCONGEN[i].U_City;
            $scope.g_curency= $scope.data.CCONGEN[i].U_Currency;
            $scope.g_egroup= $scope.data.CCONGEN[i].U_EQGroup;
            $scope.g_rate= $scope.data.CCONGEN[i].U_rate;
            $scope.g_uom= $scope.data.CCONGEN[i].U_UOM;
            $scope.g_gst= $scope.data.CCONGEN[i].U_GST;
            $scope.g_remark= $scope.data.CCONGEN[i].U_Remarks;
    }




    //add-on functions
    
    $scope.a_add = function () {

        $scope.add = {
            "U_Ctype": $scope.a_Ctype,
            "U_Continent": $scope.a_Continent,
            "U_Country": $scope.a_Country,
            "U_City": $scope.a_city,
            "U_Currency": $scope.a_curency,
            "U_EQGroup": $scope.a_egroup,
            "U_Rate": $scope.a_rate,
            "U_UOM": $scope.a_uom,
            "U_GST": $scope.a_gst,
            "U_Remarks": $scope.a_remark,
        };
        $scope.data.CCONADD.push($scope.add);
        $scope.a_reset();
    }

    $scope.a_reset = function () {

            $scope.a_Ctype = "";
            $scope.a_Continent= "";
            $scope.a_Country= "";
            $scope.a_city= "";
            $scope.a_curency= "";
            $scope.a_egroup= "";
            $scope.a_rate= "";
            $scope.a_uom= "";
            $scope.a_gst= "";
            $scope.a_remark= "";
        
        
    }


    $scope.a_edit = function(i)
    {
       // alert(i);
        $scope.data.CCONADD[i].edit = true;
    }
    $scope.a_ok = function(i)
    {
       // alert(i);
        $scope.data.CCONADD[i].edit = false;
    }

    $scope.a_del = function(i)
    {
        //alert(i);
        $scope.data.CCONADD.splice(i, 1); 
    }
     $scope.a_copy = function(i)
    { 
            $scope.a_Ctype = $scope.data.CCONADD[i].U_Ctype;
            $scope.a_Continent= $scope.data.CCONADD[i].U_Continent;
            $scope.a_Country= $scope.data.CCONADD[i].U_Country;
            $scope.a_city= $scope.data.CCONADD[i].U_City;
            $scope.a_curency= $scope.data.CCONADD[i].U_Currency;
            $scope.a_egroup= $scope.data.CCONADD[i].U_EQGroup;
            $scope.a_rate= $scope.data.CCONADD[i].U_rate;
            $scope.a_uom= $scope.data.CCONADD[i].U_UOM;
            $scope.a_gst= $scope.data.CCONADD[i].U_GST;
            $scope.a_remark= $scope.data.CCONADD[i].U_Remarks;
    }
    

    //resetall field
    $scope.resetall = function()
    {
             $scope.data = {
            "SQTO": [{
                "U_Qno": "",
                "U_Status": "",
                "U_Uname": "",
                "U_Cdate": "",
                "U_Ccode": "",
                "U_Cname": "",
                "U_CPeriod1": "",
                "U_CPeriod2": "",
                "U_Pcode": "",
                "U_AddrN": "",
                "U_Addr1": "",
                "U_Addr2": "",
                "U_Addr3": "",
                "U_Addr4": "",
                "U_Addr5": "",
                "U_Addr6": "",
                "U_TelNo": "",
                "U_FaxNo": "",
                "U_Mno": "",
                "U_Email": "",
                "U_Remarks": ""
            }],
            "CCONGEN": [],
            "CCONADD": []
        };
    }

    $scope.resetall();
    //save service call
    $scope.save = function()
    {
        $scope.savelable = "Loading..";
        $scope.savebtn = true;
        var config = {
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded;charset=utf-8;'
            }
        }

        var parms = encodeURIComponent(JSON.stringify($scope.data));
        $http.post("http://119.73.138.58:82/ICSB.asmx/CustomerContractAdd", "value=" + parms, config)
   .then(
       function (response) {
           // success callback
           console.log(response.data);
           if (response.data.VALIDATE[0].Status == "Ture") {
              $scope.savelable = "Save";
              $scope.savebtn = false;
              $scope.resetall();
              alert(response.data.VALIDATE[0].Msg);
           }
            else
            {
               alert(response.data.VALIDATE[0].Msg);
                $scope.savelable = "Save";
                $scope.savebtn = false;
               }
       },
       function (response) {
           // failure callback

       }
    );
    }

} ]);
