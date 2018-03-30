(function () {
    "use strict"; //can't have any variables we didn't define

    angular.module(appName).component("faqManager", { //appname global var   we are attaching component to app name //takes in string and object
        bindings: {}, //
        templateUrl: "/Scripts/Components/Views/FaqManagerView.html",

        controller: function (requestService, $scope, $window) {
            var vm = this;

            vm.faqItems = []; //faq categories are populated in here
            vm.$onInit = _init;

            vm.faqModel = {}; //this is the model that holds the info from the form (the form {})
            vm.postFaq = _postFaq;//post funciton EVENTS

            vm.deleteFaq = _deleteFaq;//deleting the faq

            vm.populateFaqFormWithPostedFaq = _populateFaqFormWithPostedFaq; //edit. populates the form to edit

            function _init() {//calls the categories and gets all faq items
                requestService.ApiRequestService("get", "\/api/FaqItems")
                    .then(function (response) {
                        vm.faqItems = response;
                        //console.log("Start", vm.faqItems);
                    })
                    .catch(function (err) {
                        console.log(err);
                    })
            }

            function _postFaq(form) {//submit button that is a PUT button if there is an id
                var method = vm.faqModel.id ? 'PUT' : 'POST'; //if there is an id then PUT if not then POST
                var endPt = vm.faqModel.id ? '/' + vm.faqModel.id : ''; //if there is an id then /id, if not then just nothing
                requestService.ApiRequestService(method, "/api/FaqItems" + endPt, vm.faqModel)
                    .then(function (response) {
                        //swal("Congratulations!", "Team was successfully added", "success");
                        swal({ title: "Done!", text: "FAQ was successfully added", timer: 1800, showConfirmButton: false, type: "success" });
                        vm.faqModel = {};
                        form.$setPristine();
                        form.$setUntouched();
                        _init();
                    })
                    .catch(function (err) {
                        swal({ title: "Opps!", text: "Something went wrong: " + err.data, timer: 1800, showConfirmButton: false, type: "error" });
                        console.log(err);
                    })
            }

            function _deleteFaq(id) { //deletes item
                swal({
                    title: "Are you sure you want to delete this FAQ?",
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
                            requestService.ApiRequestService("delete", "/api/FaqItems/" + id)
                                .then(function (response) {
                                    vm.faqModel = {};
                                    _init();
                                })
                                .catch(function (err) {
                                    console.log(err);
                                });
                        }
                    });

            }

            function _populateFaqFormWithPostedFaq(item) {//puts stuff back into the form
                vm.faqModel = item;
            }
        }
    })
})();