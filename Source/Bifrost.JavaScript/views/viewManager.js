﻿Bifrost.namespace("Bifrost.views", {
    viewManager: Bifrost.Singleton(function (viewFactory, pathResolvers, regionManager, UIManager, viewModelManager, viewModelLoader, documentService) {
        var self = this;


        function setViewModelForElement(element, viewModel) {
            viewModelManager.masterViewModel.setFor(element, viewModel);

            var viewModelName = documentService.getViewModelNameFor(element);

            var dataBindString = "";
            var dataBind = element.attributes.getNamedItem("data-bind");
            if (!Bifrost.isNullOrUndefined(dataBind)) {
                dataBindString = dataBind.value + ", ";
            } else {
                dataBind = document.createAttribute("data-bind");
            }
            dataBind.value = dataBindString + "viewModel: $root['" + viewModelName + "']";
            element.attributes.setNamedItem(dataBind);
        }

        
        this.initializeLandingPage = function () {
            var body = document.body;
            if (body !== null) {
                var file = Bifrost.Path.getFilenameWithoutExtension(document.location.toString());
                if (file == "") file = "index";

                if (pathResolvers.canResolve(body, file)) {
                    var actualPath = pathResolvers.resolve(body, file);
                    var view = viewFactory.createFrom(actualPath);
                    view.element = body;
                    view.content = body.innerHTML;
                    body.view = view;

                    regionManager.getFor(view).continueWith(function (region) {
                        if (viewModelManager.hasForView(actualPath)) {
                            viewModelPath = viewModelManager.getViewModelPathForView(actualPath);
                            if (!viewModelManager.isLoaded(viewModelPath)) {
                                viewModelLoader.load(viewModelPath, region).continueWith(function (viewModel) {
                                    setViewModelForElement(body, viewModel);
                                });
                            } else {
                                viewModelLoader.beginCreateInstanceOfViewModel(viewModelPath, region).continueWith(function (viewModel) {
                                    setViewModelForElement(body, viewModel);
                                });
                            }
                        }
                    
                        UIManager.handle(body);
                    });
                }
            }
        };
    })
});
Bifrost.WellKnownTypesDependencyResolver.types.viewManager = Bifrost.views.viewManager;