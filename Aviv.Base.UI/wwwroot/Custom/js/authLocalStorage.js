/**
 * Authentication utility functions for local storage
 */
window.authLocalStorage = {
    // Set authentication token
    setAuthToken: function (token) {
        localStorage.setItem('aviv_auth_token', token);
        console.log('Auth token set in localStorage');
    },

    // Get authentication token
    getAuthToken: function () {
        return localStorage.getItem('aviv_auth_token');
    },

    // Remove authentication token
    removeAuthToken: function () {
        localStorage.removeItem('aviv_auth_token');
        console.log('Auth token removed from localStorage');
    },

    // Set user info
    setUserInfo: function (userInfo) {
        if (typeof userInfo === 'object') {
            localStorage.setItem('aviv_user_info', JSON.stringify(userInfo));
        } else {
            localStorage.setItem('aviv_user_info', userInfo);
        }
        console.log('User info set in localStorage');
    },

    // Get user info
    getUserInfo: function () {
        const userInfo = localStorage.getItem('aviv_user_info');
        if (!userInfo) return null;

        return userInfo;
    },

    // Remove user info
    removeUserInfo: function () {
        localStorage.removeItem('aviv_user_info');
        console.log('User info removed from localStorage');
    },

    // Check if user is authenticated
    isAuthenticated: function () {
        return !!localStorage.getItem('aviv_auth_token');
    },

    // Clear all authentication data
    clearAuth: function () {
        this.removeAuthToken();
        this.removeUserInfo();
        console.log('All auth data cleared from localStorage');
    }
};