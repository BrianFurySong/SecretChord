(function () {
    'use strict';

    angular.module(appName).component("aboutPagePicUpload", {
        bindings: {
            completedCallbackFn: "<",
            fileRepositoryIdRequest: "<",
            api: "="
        },
        templateUrl: "/Scripts/components/views/AboutPagePicUploadView.html",
        controller: function (requestService, $scope, $window, $q) {
            var vm = this;
            vm.fileUploadModel = {};
            vm.$onInit = _init;
            vm.fileRepositoryIdRequest = "";
            vm.myImage = '';
            vm.myCroppedImage = '';
            vm.cropBlob = '';
            vm.handleFileSelect = _handleFileSelect;
            //vm.grabUrl = _grabUrl;
            vm.commit = _commit;
            vm.reject = _reject;
            vm.commitCrop = false;

            function _handleFileSelect(evt) {
                vm.file = evt.currentTarget.files[0];
                var reader = new FileReader();
                reader.onload = function (evt) {
                    $scope.$apply(function () {
                        vm.myImage = evt.target.result;
                    });
                };
                reader.readAsDataURL(vm.file);
            };

            $scope.imgLoaded = function (elem) {
                var file = elem.files[0];
                vm.fileName = file;

                if (file !== undefined) {
                    var fileReader = new FileReader();
                    fileReader.onload = function (f) {
                        vm.data = f.target.result;
                        $scope.$apply(function ($scope) {
                            vm.uploadedFile = file;
                        });
                    };
                    fileReader.readAsDataURL(file);
                }
            };

            angular.element(document.querySelector('#fileInput')).on('change', _handleFileSelect);

            //function _grabUrl() {
            //    console.log('myCroppedImage', vm.myCroppedImage);//needs conversion into blob
            //}

            function _init() {
                // Setup so that parent function can call this child function, asyncUpLoad
                vm.api = {};
                vm.api.asyncUpLoad = asyncUpLoad;
            }

            //converts img URI (cropped image to blob)
            function dataURItoBlob(dataURI) {
                //console.log("DATAURI", dataURI);
                // convert base64/URLEncoded data component to raw binary data held in a string
                var arr = dataURI.split(','), mime = arr[0].match(/:(.*?);/)[1],
                    bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
                while (n--) {
                    u8arr[n] = bstr.charCodeAt(n);
                }
                return new File([u8arr], vm.fileName, { type: mime });
            }

            function _commit() { //this is the crop button saying that u like what u see
                vm.commitCrop = true; //hides crop btn (cancel btn remains) and hides full image
            }

            function _reject() {
                vm.myCroppedImage = ''; //clears cropped image
                vm.myImage = ''; //clears image
                vm.cropFileInput = null; //allow input new file
                vm.commitCrop = false; //show image cropper
            }

            function asyncUpLoad() {
                if (vm.myCroppedImage) {//only uploads if theres a cropped img
                    vm.blobImg = dataURItoBlob(vm.myCroppedImage);
                }
                return $q(function (resolve, reject) {
                    requestService.UploadFile("/api/FileUpload", vm.blobImg, vm.fileRepositoryIdRequest)
                        .then(function (response) {
                            vm.uploadMessage = "File " + response + " was uploaded successfully.";
                            resolve(response);
                            vm.blobImg = {};
                            _reject();

                        })
                        .catch(function (error) {
                            vm.uploadMessage = "Failed to upload file. " + error.data;
                            reject("Failed to Upload File - " + error);
                        });
                });
            }

        }
    });
})();