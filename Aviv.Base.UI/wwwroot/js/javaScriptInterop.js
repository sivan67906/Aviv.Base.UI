/**
 * Optimized interop.js - Utility functions for Blazor JavaScript interop
 */
window.interop = (function () {
    // Cache DOM references to improve performance
    const docElement = document.documentElement;
    const elementCache = new Map();

    // Utility functions to reduce code duplication
    const utils = {
        // Safely get an element, optionally using cache
        getElement(selector, useCache = true) {
            if (useCache && elementCache.has(selector)) {
                return elementCache.get(selector);
            }

            const element = document.querySelector(selector);

            if (element && useCache) {
                elementCache.set(selector, element);
            }

            return element;
        },

        // Clear element cache when DOM likely changes
        clearCache() {
            elementCache.clear();
        },

        // Safely execute a function with error handling
        safeExecute(fn, errorResult = null) {
            try {
                return fn();
            } catch (error) {
                console.error(`Interop error: ${error.message}`);
                return errorResult;
            }
        },

        // Create a promise wrapper with standardized error handling
        createPromise(fn) {
            return new Promise((resolve, reject) => {
                try {
                    const result = fn();
                    resolve(result);
                } catch (error) {
                    console.error(`Interop promise error: ${error.message}`);
                    reject(error);
                }
            });
        }
    };

    // Element handling functions
    const elementFunctions = {
        getElement(elementRef) {
            return utils.createPromise(() => {
                const selector = utils.getElement(elementRef);
                return selector ? true : false;
            });
        },

        isEleExist(elementRef) {
            return utils.safeExecute(() => {
                return !!utils.getElement(elementRef, false);
            }, false);
        },

        getBoundry(elementRef) {
            return utils.safeExecute(() => {
                return elementRef?.getBoundingClientRect();
            });
        },

        addClass(elementRef, className) {
            utils.safeExecute(() => {
                const element = utils.getElement(elementRef);
                if (element) {
                    element.classList.add(className);
                }
            });
        },

        removeClass(elementRef, className) {
            utils.safeExecute(() => {
                const element = utils.getElement(elementRef);
                if (element) {
                    element.classList.remove(className);
                }
            });
        },

        getAttribute(elementRef, attributeName) {
            return utils.createPromise(() => {
                const element = utils.getElement(elementRef);
                if (!element) throw new Error("Element not found");
                return element.getAttribute(attributeName);
            });
        },

        setAttribute(elementRef, attributeName, attributeValue) {
            return utils.createPromise(() => {
                const element = utils.getElement(elementRef);
                if (!element) throw new Error("Element not found");
                return element.setAttribute(attributeName, attributeValue);
            });
        }
    };

    // Menu navigation specific functions
    const menuFunctions = {
        MenuNavElement(elementRef) {
            return utils.createPromise(() => {
                const element = utils.getElement(elementRef);
                if (!element) throw new Error("Element not found");

                const computedStyle = window.getComputedStyle(element);
                const scrollWidth = element.scrollWidth;
                const marginInlineStart = Math.ceil(
                    Number(computedStyle.marginInlineStart.split("px")[0])
                );

                return { scrollWidth, marginInlineStart };
            });
        },

        MenuNavmarginInlineStart(selector, value) {
            return utils.createPromise(() => {
                const element = utils.getElement(selector);
                if (!element) throw new Error("Element not found");

                element.style.marginInlineStart = value;
                return element;
            });
        },

        mainSidebarOffset(elementRef) {
            return utils.createPromise(() => {
                const element = utils.getElement(elementRef);
                if (!element) throw new Error("Element not found");

                return element.offsetWidth;
            });
        },

        directionChange(dataId) {
            return utils.safeExecute(() => {
                if (!dataId) return false;

                const element = document.querySelector(`[data-id="${dataId}"]`);
                if (!element) return false;

                const listItem = element.closest("li");
                if (!listItem) return false;

                const siblingUL = listItem.querySelector("ul");
                if (!siblingUL) return false;

                // Calculate outer UL width
                let outterUlWidth = 0;
                let listItemUL = listItem.closest("ul:not(.main-menu)");
                while (listItemUL) {
                    listItemUL = listItemUL.parentElement.closest("ul:not(.main-menu)");
                    if (listItemUL) {
                        outterUlWidth += listItemUL.clientWidth;
                    }
                }

                // Get element boundaries
                const siblingULRect = listItem.getBoundingClientRect();
                const isRTL = docElement.getAttribute("dir") === "rtl";
                const windowWidth = window.innerWidth;

                if (isRTL) {
                    return siblingULRect.left - siblingULRect.width - outterUlWidth + 150 < 0 &&
                        outterUlWidth < windowWidth &&
                        outterUlWidth + siblingULRect.width * 2 < windowWidth;
                } else {
                    return outterUlWidth + siblingULRect.right + siblingULRect.width + 50 > windowWidth &&
                        siblingULRect.right >= 0 &&
                        outterUlWidth + siblingULRect.width * 2 < windowWidth;
                }
            }, false);
        },

        groupDirChange() {
            return utils.safeExecute(() => {
                const elemList = {
                    added: [],
                    removed: [],
                    clearNavDropdown: false
                };

                const isHorizontalLayout = docElement.getAttribute("data-nav-layout") === "horizontal";
                const isDesktopWidth = window.innerWidth > 992;

                if (!isHorizontalLayout || !isDesktopWidth) {
                    return elemList;
                }

                // Process active menus
                const activeMenus = document.querySelectorAll(".slide.has-sub.open > ul");
                activeMenus.forEach(processActiveMenu);

                // Process forced-left items
                processLeftForceItem();

                // Process all submenu elements
                processSubmenuElements();

                // Remove duplicates
                elemList.added = [...new Set(elemList.added)];
                elemList.removed = [...new Set(elemList.removed)];

                return elemList;

                // Helper functions
                function processActiveMenu(target) {
                    const html = docElement;
                    const listItem = target.closest("li");

                    if (!listItem) return;

                    const dropdownRect = listItem.getBoundingClientRect();
                    const dropdownWidth = target.getBoundingClientRect().width;
                    const rightEdge = dropdownRect.right + dropdownWidth;
                    const leftEdge = dropdownRect.left - dropdownWidth;

                    const isRTL = html.getAttribute("dir") === "rtl";

                    if (isRTL) {
                        handleRTLMenu(target, dropdownRect, rightEdge, leftEdge, listItem);
                    } else {
                        handleLTRMenu(target, dropdownRect, rightEdge, leftEdge, listItem);
                    }
                }

                function handleRTLMenu(target, dropdownRect, rightEdge, leftEdge, listItem) {
                    // Check for child1 menus
                    if (target.classList.contains("child1") && dropdownRect.left < 0) {
                        elemList.clearNavDropdown = true;
                    }

                    // Handle edge positioning
                    if (leftEdge < 0) {
                        elemList.added.push(
                            target.previousElementSibling.getAttribute("data-id")
                        );
                    } else if (
                        listItem.closest("ul")?.classList.contains("force-left") &&
                        rightEdge < window.innerWidth
                    ) {
                        elemList.added.push(
                            target.previousElementSibling.getAttribute("data-id")
                        );
                    } else {
                        elemList.removed.push(
                            target.previousElementSibling.getAttribute("data-id")
                        );
                    }
                }

                function handleLTRMenu(target, dropdownRect, rightEdge, leftEdge, listItem) {
                    // Check for child1 menus
                    if (target.classList.contains("child1") && dropdownRect.right > window.innerWidth) {
                        elemList.clearNavDropdown = true;
                    }

                    // Handle edge positioning
                    if (rightEdge > window.innerWidth) {
                        elemList.added.push(
                            target.previousElementSibling.getAttribute("data-id")
                        );
                    } else if (
                        listItem.closest("ul")?.classList.contains("force-left") &&
                        leftEdge > 0
                    ) {
                        elemList.added.push(
                            target.previousElementSibling.getAttribute("data-id")
                        );
                    } else if (leftEdge < 0) {
                        elemList.removed.push(
                            target.previousElementSibling.getAttribute("data-id")
                        );
                    } else {
                        elemList.removed.push(
                            target.previousElementSibling.getAttribute("data-id")
                        );
                    }
                }

                function processLeftForceItem() {
                    const leftForceItem = document.querySelector(".slide-menu.active.force-left");
                    if (!leftForceItem) return;

                    const isRTL = docElement.getAttribute("dir") === "rtl";

                    if (!isRTL) {
                        const rightEdge = leftForceItem.getBoundingClientRect().right;
                        const leftEdge = leftForceItem.getBoundingClientRect().left;

                        if (rightEdge < window.innerWidth) {
                            elemList.removed.push(
                                leftForceItem.previousElementSibling.getAttribute("data-id")
                            );
                        } else if (leftEdge < 0) {
                            const navStyle = docElement.getAttribute("data-nav-style");
                            if (
                                navStyle === "menu-hover" ||
                                navStyle === "icon-hover" ||
                                window.innerWidth > 992
                            ) {
                                elemList.removed.push(
                                    leftForceItem.previousElementSibling.getAttribute("data-id")
                                );
                            }
                        }
                    } else {
                        const parentMenu = leftForceItem.parentElement.closest(".slide-menu");
                        const leftEdge = leftForceItem.getBoundingClientRect().left;
                        const width = leftForceItem.getBoundingClientRect().width;

                        const check = leftEdge - (parentMenu?.clientWidth || 0) - width;

                        if (check > 0) {
                            const navStyle = docElement.getAttribute("data-nav-style");
                            if (
                                navStyle === "menu-hover" ||
                                navStyle === "icon-hover" ||
                                window.innerWidth > 992
                            ) {
                                elemList.removed.push(
                                    leftForceItem.previousElementSibling.getAttribute("data-id")
                                );
                            }
                        }
                    }
                }

                function processSubmenuElements() {
                    const elements = document.querySelectorAll(".main-menu .has-sub ul");
                    elements.forEach(e => {
                        if (!isElementVisible(e)) return;

                        const rect = e.getBoundingClientRect();
                        const isRTL = docElement.getAttribute("dir") === "rtl";

                        if (isRTL && rect.left < 0) {
                            if (e.classList.contains("child1")) {
                                elemList.removed.push(
                                    e.previousElementSibling.getAttribute("data-id")
                                );
                            } else {
                                elemList.added.push(
                                    e.previousElementSibling.getAttribute("data-id")
                                );
                            }
                        } else if (!isRTL && rect.right > window.innerWidth) {
                            if (e.classList.contains("child1")) {
                                elemList.removed.push(
                                    e.previousElementSibling.getAttribute("data-id")
                                );
                            } else {
                                elemList.added.push(
                                    e.previousElementSibling.getAttribute("data-id")
                                );
                            }
                        }
                    });
                }
            }, { added: [], removed: [], clearNavDropdown: false });
        }
    };

    // Window and viewport functions
    const windowFunctions = {
        inner(arg) {
            if (arg === "innerWidth") {
                return window.innerWidth ?? 992;
            }
            if (arg === "innerHeight") {
                return window.innerHeight ?? 992;
            }
            return 0;
        },

        // UPDATED FUNCTION: Improved scroll visibility update with error handling
        updateScrollVisibility(dotnetHelper) {
            // Remove any existing scroll handler
            if (window._visibilityScrollHandler) {
                window.removeEventListener('scroll', window._visibilityScrollHandler);
            }

            // Create new handler with proper error handling
            window._visibilityScrollHandler = function () {
                try {
                    // Ensure we're passing a valid integer
                    const scrollHeight = Math.round(window.scrollY || 0);

                    // Only call if dotnetHelper is still valid
                    if (dotnetHelper && typeof dotnetHelper.invokeMethodAsync === 'function') {
                        dotnetHelper.invokeMethodAsync('UpdateScrollVisibility', scrollHeight);
                    }
                } catch (error) {
                    console.error("Error updating scroll visibility:", error);
                }
            };

            // Use addEventListener instead of onscroll for better cleanup
            window.addEventListener('scroll', window._visibilityScrollHandler);
        },

        scrollToTop() {
            window.scrollTo({
                top: 0,
                behavior: "smooth"
            });
        },

        // UPDATED FUNCTION: Improved scroll listener with error handling
        registerScrollListener(dotnetHelper) {
            // Remove any existing listener first
            if (window._scrollHandler) {
                window.removeEventListener('scroll', window._scrollHandler);
            }

            // Create and store the handler for later cleanup
            window._scrollHandler = function () {
                try {
                    // Ensure scrollY is a valid integer
                    const scrollY = Math.round(window.scrollY || 0);

                    // Only call if dotnetHelper is still valid
                    if (dotnetHelper && typeof dotnetHelper.invokeMethodAsync === 'function') {
                        dotnetHelper.invokeMethodAsync("SetStickyClass", scrollY);
                    }
                } catch (error) {
                    console.error("Error in scroll handler:", error);
                }
            };

            window.addEventListener('scroll', window._scrollHandler);

            // Trigger initial check
            window._scrollHandler();
        },

        // UPDATED FUNCTION: Improved detach method for scroll listener
        detachScrollListener() {
            if (window._scrollHandler) {
                window.removeEventListener('scroll', window._scrollHandler);
                window._scrollHandler = null;
            }
        },

        // NEW FUNCTION: Method to detach all scroll handlers
        detachAllScrollListeners() {
            if (window._scrollHandler) {
                window.removeEventListener('scroll', window._scrollHandler);
                window._scrollHandler = null;
            }

            if (window._visibilityScrollHandler) {
                window.removeEventListener('scroll', window._visibilityScrollHandler);
                window._visibilityScrollHandler = null;
            }
        }
    };

    // HTML document-level functions
    const htmlFunctions = {
        getMultipleAttributes(attributeNames) {
            return utils.safeExecute(() => {
                if (!Array.isArray(attributeNames)) {
                    throw new TypeError("attributeNames must be an array");
                }

                const result = {};
                attributeNames.forEach(attribute => {
                    result[attribute] = docElement.getAttribute(attribute) || '';
                });
                return result;
            }, {});
        },

        addClassToHtml(className) {
            docElement.classList.add(className);
        },

        removeClassFromHtml(className) {
            docElement.classList.remove(className);
        },

        getAttributeToHtml(attributeName) {
            return docElement.getAttribute(attributeName);
        },

        addAttributeToHtml(attributeName, attributeValue) {
            docElement.setAttribute(attributeName, attributeValue);
            // Clear cache since HTML attributes may affect element styles
            utils.clearCache();
        },

        removeAttributeFromHtml(attributeName) {
            docElement.removeAttribute(attributeName);
            // Clear cache since HTML attributes may affect element styles
            utils.clearCache();
        },

        setclearCssVariables() {
            docElement.style = "";
            // Clear cache since styling changed
            utils.clearCache();
        },

        setCssVariable(variableName, value) {
            docElement.style.setProperty(variableName, value);
        },

        removeCssVariable(variableName) {
            docElement.style.removeProperty(variableName);
        },

        setCustomCssVariable(element, variableName, value) {
            const ele = utils.getElement(element);
            if (ele) {
                ele.style.setProperty(variableName, value);
            }
        }
    };

    // Local storage functions
    const storageFunctions = {
        setLocalStorageItem(key, value) {
            localStorage.setItem(key, value);
        },

        getLocalStorageItem(key) {
            return localStorage.getItem(key);
        },

        removeLocalStorageItem(key) {
            localStorage.removeItem(key);
        },

        getAllLocalStorageItem() {
            return localStorage;
        },

        clearAllLocalStorage() {
            localStorage.clear();
        }
    };

    // Bootstrap initialization functions
    const bootstrapFunctions = {
        initializeTooltips() {
            utils.safeExecute(() => {
                if (typeof bootstrap === 'undefined' || !bootstrap.Tooltip) {
                    console.warn('Bootstrap Tooltip not available');
                    return;
                }

                const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
                [...tooltipTriggerList].map(tooltipTriggerEl =>
                    new bootstrap.Tooltip(tooltipTriggerEl));
            });
        },

        initializePopover() {
            utils.safeExecute(() => {
                if (typeof bootstrap === 'undefined' || !bootstrap.Popover) {
                    console.warn('Bootstrap Popover not available');
                    return;
                }

                const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]');
                [...popoverTriggerList].map(popoverTriggerEl =>
                    new bootstrap.Popover(popoverTriggerEl));
            });
        },

        initCardRemove() {
            utils.safeExecute(() => {
                const DIV_CARD = ".card";
                const cardRemoveBtn = document.querySelectorAll('[data-bs-toggle="card-remove"]');

                cardRemoveBtn.forEach(ele => {
                    ele.addEventListener("click", function (e) {
                        e.preventDefault();
                        const card = this.closest(DIV_CARD);
                        if (card) {
                            card.remove();
                        }
                        return false;
                    });
                });
            });
        },

        initCardFullscreen() {
            utils.safeExecute(() => {
                const DIV_CARD = ".card";
                const cardFullscreenBtn = document.querySelectorAll('[data-bs-toggle="card-fullscreen"]');

                cardFullscreenBtn.forEach(ele => {
                    ele.addEventListener("click", function (e) {
                        e.preventDefault();
                        const card = this.closest(DIV_CARD);
                        if (card) {
                            card.classList.toggle("card-fullscreen");
                            card.classList.remove("card-collapsed");
                        }
                        return false;
                    });
                });
            });
        }
    };

    // Merge all function groups into a single API
    return {
        ...elementFunctions,
        ...menuFunctions,
        ...windowFunctions,
        ...htmlFunctions,
        ...storageFunctions,
        ...bootstrapFunctions,

        // Add a method to dispose DotNet references
        disposeDotNetReference(dotNetRef) {
            if (dotNetRef && typeof dotNetRef.dispose === 'function') {
                dotNetRef.dispose();
            }
        }
    };
})();

// Utility function for checking element visibility
function isElementVisible(element) {
    if (!element) return false;
    const computedStyle = window.getComputedStyle(element);
    return computedStyle.display !== "none";
}

// Function to smoothly apply theme transitions
window.themeHelper = {
    // Apply visual feedback when switching to published form theme
    applyPublishedFormTheme: function () {
        // Add a brief transition effect to indicate theme change
        document.body.classList.add('theme-transition');

        // Use setTimeout to create a slight delay for visual effect
        setTimeout(() => {
            // Remove transition class after change is complete
            document.body.classList.remove('theme-transition');
        }, 500);

        // Scroll to top for better user experience
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });

        // Return true to indicate success
        return true;
    },

    // Helper to check current theme
    isHorizontalLayout: function () {
        return document.documentElement.getAttribute('data-nav-layout') === 'horizontal';
    },

    // Helper to get menu height for spacing adjustments
    getMenuHeight: function () {
        const menuElement = document.querySelector('.app-sidebar');
        return menuElement ? menuElement.offsetHeight : 0;
    }
};

// Add this CSS to handle the transition effect
document.addEventListener('DOMContentLoaded', function () {
    const style = document.createElement('style');
    style.textContent = `
        .theme-transition {
            transition: background-color 0.5s ease, color 0.5s ease;
            animation: theme-pulse 0.5s ease;
        }
        
        @keyframes theme-pulse {
            0% { opacity: 1; }
            50% { opacity: 0.8; }
            100% { opacity: 1; }
        }
    `;
    document.head.appendChild(style);
});