(function () {
    "use strict";

    angular.module(appName).component("appConfigManager", {
        bindings: {},
        templateUrl: "/scripts/components/views/appConfigManagerView.html",
        controller: function (requestService, $scope, $window) { //put BLING in front of the injected params EXCEPT for requestService
            var vm = this;
            vm.$onInit = _init; //use a bling after vm.
            vm.appConfigItems = [];
            vm.appconfigModel = {};
            vm.editValue = _editValue;
            vm.showDiv = _showDiv;

            function _init() {
                requestService.ApiRequestService("get", "/api/AppConfigs")
                    .then(function (response) {
                        vm.appConfigItems = response;

                        vm.appConfigItems.forEach(function (obj) {
                            obj.show = true;
                            obj.hide = true;
                        });

                        //this does the same thing as above. just trying a forEach loop instead of a for loop
                        //for (var i = 0; i < vm.appConfigItems.length; i++) {
                        //    vm.appConfigItems[i].show = true;
                        //    vm.appConfigItems[i].hide = true;
                        //}
                    })
                    .catch(function (err) {
                        console.log(err);
                    })
            }

            function _editValue(item) {
                delete item.show;
                delete item.hide;
                requestService.ApiRequestService("put", "/api/AppConfigs/" + item.id, item)
                    .then(function (response) {
                        _init();
                    })
                    .catch(function (err) {
                        console.log(err);
                    })
            }

            function _showDiv(item) {
                var index = vm.appConfigItems.indexOf(item);
                if (item && vm.appConfigItems[index].show == true && vm.appConfigItems[index].hide == true) {
                    vm.appConfigItems[index].show = false;
                    vm.appConfigItems[index].hide = false;
                } else if (item && vm.appConfigItems[index].show == false && vm.appConfigItems[index].hide == false) {
                    _init();
                }
            }
        }
    })
})();