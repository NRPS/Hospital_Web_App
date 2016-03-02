'use strict';

define([], function () {
    function runApp($rootScope, $location, $cookieStore, $http) {
        // keep user logged in after page refresh
        $rootScope.globals = $cookieStore.get('globals') || {};
        if ($rootScope.globals.currentUser) {
            $http.defaults.headers.common['Authorization'] = 'Basic ' + $rootScope.globals.currentUser.authdata; // jshint ignore:line
        }

        $rootScope.$on('$locationChangeStart', function (event, next, current) {
            // redirect to login page if not logged in
            if ($location.path() !== '/Login' && !$rootScope.globals.currentUser) {
                $location.path('/Login');
            }
        });
    }
    runApp.$inject = ['$rootScope', '$location', '$cookieStore', '$http'];
    return runApp;
})
