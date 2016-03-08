
'use strict';
require.config({
    paths: {
        'angular': 'bower_components/angular/angular.min',
        //'angularAMD': 'bower_components/angularAMD/angularAMD.min',
        'domReady': 'bower_components/domReady/domReady',
        'ngRoute': 'bower_components/angular-route/angular-route.min',
        'ngCookies': 'bower_components/angular-cookies/angular-cookies.min',
        'jquery': 'bower_components/jquery/dist/jquery.min',
        'bootstrap': 'bower_components/bootstrap/dist/js/bootstrap.min',
        
    },
    shim: {
        'angular': {
            'angular': { 'exports': 'angular', deps: ['jQuery'] },
        },
        'angularAMD': {
            deps: ['angular']
        },
        'ngRoute': {
            deps: ['angular']
        },
        'ngCookies': {
            deps: ['angular']
        },
        'jquery': {

        },
        'bootstrap': {
            deps: ['jquery']
        }
    }
});
require(['ngRoute', 'ngCookies', 'bootstrap','JS/app'],
    function () {
        'use strict';
        require(['domReady','angular'], function (angular) {
           angular.bootstrap(document, ['hospital']);
        });
    },
     function (err) {
         var failedId = err;
         console.log(failedId)
         if (failedId === 'JS/app') {
             console.log("hiii");
                requirejs.config({
             paths: {
             'app': 'JS/app'
         }
         });
         }
     } );

