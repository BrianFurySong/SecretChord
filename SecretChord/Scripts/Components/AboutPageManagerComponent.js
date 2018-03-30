(function () {
    "use strict";

    angular.module(appName).component("aboutPageManager", {
        bindings: {},
        templateUrl: "/Scripts/Components/Views/AboutPageManagerView.html",
        controller: function (requestService, $scope, $window) {
            var vm = this;
            vm.$onInit = _init;
            vm.aboutModel = {};
            vm.allAbouts = [];
            vm.editAbout = _editAbout;
            vm.postAbout = _postAbout;
            vm.putAbout = _putAbout;
            vm.deleteAbout = _deleteAbout;
            vm.clearPic = _clearPic;
            vm.fileName = "";

            function _init() {
                requestService.ApiRequestService("get", "/api/AboutPages")
                    .then(function (response) {
                        vm.allAbouts = [];
                        vm.allAbouts = response;
                        console.log("allabouts", vm.allAbouts);
                    })
                    .catch(function (err) {
                        console.log(err);
                    })
            }

            function _postAbout(form) {
                vm.gridApi.asyncUpLoad()//this is the pic upload function and on success call the api request call
                    .then(function (responseAsyncUpload) {
                        vm.fileName = responseAsyncUpload;
                        vm.aboutModel.imageURL = vm.fileName;
                        requestService.ApiRequestService("post", "/api/AboutPages", vm.aboutModel)
                            .then(function (response) {
                                form.$setPristine();
                                form.$setUntouched();
                                vm.aboutModel = {};
                                swal({ title: "Congratulations!", text: "Successfully uploaded to the front page", timer: 2000, showConfirmButton: false, type: "success" });

                                _init();

                            })
                            .catch(function (err) {
                                swal("Opps!", "Something went wrong: " + err.data, "error");
                            })

                    })
                    .catch(function (err) {
                        swal("Opps!", "Something went wrong: " + err.data, "error");

                    })



            }

            function _putAbout(form) {//PUT button. if there is an imageURL it just PUTs with the model and keeps the pic
                if (vm.aboutModel.imageURL) {
                    requestService.ApiRequestService("put", "/api/AboutPages/" + vm.aboutModel.id, vm.aboutModel)
                        .then(function (response) {
                            vm.aboutModel = {};
                            form.$setPristine();
                            form.$setUntouched();
                            $scope.showUpdateBtn = false;
                            $scope.hideSubmitBtn = false;
                            $scope.showPicClearBtn = false;

                            swal({ title: "Success!", text: "Successfully edited", timer: 2000, showConfirmButton: false, type: "success" });

                            _init();
                        })
                        .catch(function (err) {
                            swal("Opps!", "Something went wrong: " + err.data, "error");

                        })
                } else {
                    vm.gridApi.asyncUpLoad()//if not it uploads a new pic
                        .then(function (responseAsyncUpload) {
                            vm.fileName = responseAsyncUpload;
                            vm.aboutModel.imageURL = vm.fileName;

                            requestService.ApiRequestService("put", "/api/AboutPages/" + vm.aboutModel.id, vm.aboutModel)
                                .then(function (response) {
                                    vm.aboutModel = {};
                                    form.$setPristine();
                                    form.$setUntouched();
                                    $scope.showUpdateBtn = false;
                                    $scope.hideSubmitBtn = false;
                                    $scope.showPicClearBtn = false;

                                    swal({ title: "Success!", text: "Successfully edited", timer: 2000, showConfirmButton: false, type: "success" });

                                    _init();
                                })
                                .catch(function (err) {
                                    swal("Opps!", "Something went wrong: " + err.data, "error");

                                })

                        })
                        .catch(function (err) {
                            swal("Opps!", "Something went wrong: " + err.data, "error");

                        })
                }

            }

            function _editAbout(item) {
                vm.aboutModel = item; //sets the item into the model
                if (vm.aboutModel.imageURL) { //if in the model there is an imageURL then it'll show the submit button and a clear picture button
                    $scope.showUpdateBtn = true;
                    $scope.hideSubmitBtn = true;
                    $scope.showPicClearBtn = true;
                } else { //if there isn't an imageURL, then its not going to show the clear button
                    $scope.showUpdateBtn = true;
                    $scope.hideSubmitBtn = true;
                }
            }

            function _deleteAbout(id) {
                swal({
                    title: "Are you sure you want to delete this feature?",
                    //text: "Note: You cannot edit your testimonial once you submit.",
                    type: "error",
                    showCancelButton: true,
                    confirmButtonColor: "#FF0000",
                    confirmButtonText: "Okay",
                    closeOnCancel: true,
                    closeOnConfirm: true
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            requestService.ApiRequestService("delete", "/api/AboutPages/" + id)
                                .then(function (response) {
                                    vm.aboutModel = {};
                                    vm.allAbouts = [];
                                    _init();

                                })
                                .catch(function (err) {
                                    console.log(err);

                                });
                        }
                    });

            }

            function _clearPic() {//this is the button that shows when there is an imageURL. gives you the option to clear the pic so u can uplaod a new one. if you dont clear it then it just keeps the old one
                swal({
                    title: "Are you sure you want to clear this picture?",
                    //text: "Note: You w.",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#FFAA00",
                    confirmButtonText: "Okay",
                    closeOnCancel: true,
                    closeOnConfirm: true
                },
                    function (isConfirm) {
                        if (isConfirm) {//on the success of the sweet alert
                            $scope.$apply(function () {
                                vm.aboutModel.imageURL = null;//clears the model.imageURL
                                $scope.showPicClearBtn = false;//hides the clear pic button to let the user know that they cleared it

                            })
                        }
                    });
            }
        }
    })
})();