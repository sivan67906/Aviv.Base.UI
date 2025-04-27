/**
 * Authentication utility functions for local storage
 */
window.authLocalStorage = {
    // Set authentication token
    setAuthToken: function (token) {
        localStorage.setItem('aviv_auth_token', token);
    },

    // Get authentication token
    getAuthToken: function () {
        return localStorage.getItem('aviv_auth_token');
    },

    // Remove authentication token
    removeAuthToken: function () {
        localStorage.removeItem('aviv_auth_token');
    },

    // Set user info
    setUserInfo: function (userInfo) {
        if (typeof userInfo === 'object') {
            localStorage.setItem('aviv_user_info', JSON.stringify(userInfo));
        } else {
            localStorage.setItem('aviv_user_info', userInfo);
        }
    },

    // Get user info
    getUserInfo: function () {
        const userInfo = localStorage.getItem('aviv_user_info');
        if (!userInfo) return null;

        try {
            return JSON.parse(userInfo);
        } catch (e) {
            console.error('Error parsing user info from localStorage', e);
            return userInfo;
        }
    },

    // Remove user info
    removeUserInfo: function () {
        localStorage.removeItem('aviv_user_info');
    },

    // Check if user is authenticated
    isAuthenticated: function () {
        return !!localStorage.getItem('aviv_auth_token');
    },

    // Clear all authentication data
    clearAuth: function () {
        this.removeAuthToken();
        this.removeUserInfo();
    }
};