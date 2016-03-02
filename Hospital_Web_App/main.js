
'use strict';
require.config({
    paths: {
        'angular': 'bower_components/angular/angular.min',
       // 'angularAMD': 'bower_components/angularAMD/angularAMD.min',
       // 'domReady': 'bower_components/domReady/domReady',
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

        angular.bootstrap(document, ['hospital']);
    }
);

