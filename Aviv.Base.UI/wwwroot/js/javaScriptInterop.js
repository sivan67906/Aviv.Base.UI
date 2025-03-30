// Enhanced JavaScript interop for Aviv.Base.UI
window.interop = (function () {
    // Cache for frequently accessed values to reduce DOM queries
    const cache = {
        elements: new Map(),
        attributes: new Map(),
        windowSize: { width: window.innerWidth, height: window.innerHeight }
    };

    // Refresh cache on window resize
    window.addEventListener('resize', () => {
        cache.windowSize.width = window.innerWidth;
        cache.windowSize.height = window.innerHeight;
        // Clear element cache on resize as positions may change
        cache.elements.clear();
    });

    return {
        // Get multiple HTML attributes in a single call to reduce JS interop overhead
        getMultipleAttributes: function (attributeNames) {
            // Ensure that attributeNames is an array
            if (!Array.isArray(attributeNames[0])) {
                throw new TypeError("attributeNames must be an array");
            }

            const result = {};
            attributeNames[0].forEach(attribute => {
                const cacheKey = `html-${attribute}`;
                if (cache.attributes.has(cacheKey)) {
                    result[attribute] = cache.attributes.get(cacheKey);
                } else {
                    const value = document.documentElement.getAttribute(attribute) || '';
                    cache.attributes.set(cacheKey, value);
                    result[attribute] = value;
                }
            });
            return result;
        },

        // Check if an element exists
        getElement: function (elementRef) {
            // Function to get the CSS selector for the given element reference
            return new Promise((resolve, reject) => {
                try {
                    if (cache.elements.has(elementRef)) {
                        resolve(true);
                        return;
                    }

                    const selector = document.querySelector(elementRef);
                    if (selector) {
                        cache.elements.set(elementRef, true);
                        resolve(true);
                    } else {
                        resolve(false);
                    }
                } catch (error) {
                    reject(error);
                }
            });
        },

        // Check if element exists without Promise overhead
        isEleExist: function (elementRef) {
            if (cache.elements.has(elementRef)) {
                return cache.elements.get(elementRef);
            }

            const selector = document.querySelector(elementRef);
            const exists = !!selector;
            cache.elements.set(elementRef, exists);
            return exists;
        },

        // Get element boundary
        getBoundry: function (elementRef) {
            if (!elementRef) return null;
            const rect = elementRef.getBoundingClientRect();
            return rect;
        },

        // Get window inner dimensions with cache
        inner: function (arg) {
            if (arg === "innerWidth") {
                return cache.windowSize.width || 992;
            }
            if (arg === "innerHeight") {
                return cache.windowSize.height || 992;
            }
            return 0;
        },

        // Get menu element properties - optimized with error handling
        MenuNavElement: function (elementRef) {
            return new Promise((resolve, reject) => {
                try {
                    const element = document.querySelector(elementRef);
                    if (element) {
                        const scrollWidth = element.scrollWidth;
                        const marginInlineStart = Math.ceil(
                            Number(
                                window.getComputedStyle(element).marginInlineStart.split("px")[0]
                            )
                        );
                        resolve({ scrollWidth, marginInlineStart });
                    } else {
                        reject("Element not found: " + elementRef);
                    }
                } catch (error) {
                    reject("Error in MenuNavElement: " + error.message);
                }
            });
        },

        // Set menu margin inline start - optimized with error handling
        MenuNavmarginInlineStart: function (selector, value) {
            return new Promise((resolve, reject) => {
                try {
                    const element = document.querySelector(selector);
                    if (element) {
                        element.style.marginInlineStart = value;
                        resolve(element);
                    } else {
                        reject("Element not found: " + selector);
                    }
                } catch (error) {
                    reject("Error in MenuNavmarginInlineStart: " + error.message);
                }
            });
        },

        // Get main sidebar offset - optimized with error handling
        mainSidebarOffset: function (elementRef) {
            return new Promise((resolve, reject) => {
                try {
                    const element = document.querySelector(elementRef);
                    if (element) {
                        const mainSidebarOffset = element.offsetWidth;
                        resolve(mainSidebarOffset);
                    } else {
                        reject("Element not found: " + elementRef);
                    }
                } catch (error) {
                    reject("Error in mainSidebarOffset: " + error.message);
                }
            });
        },

        // Optimized class manipulation functions
        addClass: function (elementRef, className) {
            try {
                const element = document.querySelector(elementRef);
                if (element) {
                    element.classList.add(className);
                    // Update element cache
                    cache.elements.set(elementRef, true);
                }
            } catch (error) {
                console.error("Error in addClass:", error);
            }
        },

        removeClass: function (elementRef, className) {
            try {
                const element = document.querySelector(elementRef);
                if (element) {
                    element.classList.remove(className);
                    // Update element cache
                    cache.elements.set(elementRef, true);
                }
            } catch (error) {
                console.error("Error in removeClass:", error);
            }
        },

        // HTML class management
        addClassToHtml: (className) => {
            document.documentElement.classList.add(className);
        },

        // CSS variable management - optimized
        setclearCssVariables: function () {
            document.documentElement.style = "";
            // Clear CSS variable cache
            cache.attributes.clear();
        },

        setCssVariable: function (variableName, value) {
            document.documentElement.style.setProperty(variableName, value);
            // Update cache
            cache.attributes.set(`css-${variableName}`, value);
        },

        removeCssVariable: function (variableName) {
            document.documentElement.style.removeProperty(variableName);
            // Update cache
            cache.attributes.delete(`css-${variableName}`);
        },

        setCustomCssVariable: function (element, variableName, value) {
            try {
                const ele = document.querySelector(element);
                if (ele) {
                    ele.style.setProperty(variableName, value);
                }
            } catch (error) {
                console.error("Error in setCustomCssVariable:", error);
            }
        },

        // HTML class removal
        removeClassFromHtml: (className) => {
            document.documentElement.classList.remove(className);
        },

        // HTML attribute management - with caching
        getAttributeToHtml: (attributeName) => {
            const cacheKey = `html-${attributeName}`;
            if (cache.attributes.has(cacheKey)) {
                return cache.attributes.get(cacheKey);
            }

            const value = document.documentElement.getAttribute(attributeName);
            cache.attributes.set(cacheKey, value);
            return value;
        },

        addAttributeToHtml: (attributeName, attributeValue) => {
            document.documentElement.setAttribute(attributeName, attributeValue);
            // Update cache
            cache.attributes.set(`html-${attributeName}`, attributeValue);
        },

        removeAttributeFromHtml: (attributeName) => {
            document.documentElement.removeAttribute(attributeName);
            // Update cache
            cache.attributes.delete(`html-${attributeName}`);
        },

        // Element attribute management - with error handling
        getAttribute: function (elementRef, attributeName) {
            return new Promise((resolve, reject) => {
                try {
                    const selector = document.querySelector(elementRef);
                    if (selector) {
                        resolve(selector.getAttribute(attributeName));
                    } else {
                        reject("Element not found: " + elementRef);
                    }
                } catch (error) {
                    reject("Error in getAttribute: " + error.message);
                }
            });
        },

        setAttribute: function (elementRef, attributeName, attributeValue) {
            return new Promise((resolve, reject) => {
                try {
                    const selector = document.querySelector(elementRef);
                    if (selector) {
                        selector.setAttribute(attributeName, attributeValue);
                        resolve(true);
                    } else {
                        reject("Element not found: " + elementRef);
                    }
                } catch (error) {
                    reject("Error in setAttribute: " + error.message);
                }
            });
        },

        // LocalStorage management - optimized
        setLocalStorageItem: function (key, value) {
            try {
                localStorage.setItem(key, value);
            } catch (error) {
                console.error("Error in setLocalStorageItem:", error);
            }
        },

        removeLocalStorageItem: function (key) {
            try {
                localStorage.removeItem(key);
            } catch (error) {
                console.error("Error in removeLocalStorageItem:", error);
            }
        },

        getAllLocalStorageItem: function () {
            return localStorage;
        },

        getLocalStorageItem: function (key) {
            try {
                return localStorage.getItem(key);
            } catch (error) {
                console.error("Error in getLocalStorageItem:", error);
                return null;
            }
        },

        clearAllLocalStorage: function () {
            try {
                localStorage.clear();
            } catch (error) {
                console.error("Error in clearAllLocalStorage:", error);
            }
        },

        // Direction change detection - optimized with memoization
        directionChangeMemo: new Map(),

        directionChange: function (dataId) {
            // Check memoization cache first
            const cacheKey = `dir-${dataId}-${window.innerWidth}-${document.documentElement.getAttribute("dir")}`;
            if (this.directionChangeMemo.has(cacheKey)) {
                return this.directionChangeMemo.get(cacheKey);
            }

            try {
                let element = document.querySelector(`[data-id="${dataId}"]`);
                let html = document.documentElement;
                if (!element) {
                    return false;
                }

                const listItem = element.closest("li");
                if (!listItem) {
                    return false;
                }

                // Find the first sibling <ul> element
                const siblingUL = listItem.querySelector("ul");
                let outterUlWidth = 0;
                let listItemUL = listItem.closest("ul:not(.main-menu)");

                while (listItemUL) {
                    listItemUL = listItemUL.parentElement.closest("ul:not(.main-menu)");
                    if (listItemUL) {
                        outterUlWidth += listItemUL.clientWidth;
                    }
                }

                if (siblingUL) {
                    // You've found the sibling <ul> element
                    let siblingULRect = listItem.getBoundingClientRect();
                    let result = false;

                    if (html.getAttribute("dir") == "rtl") {
                        result = siblingULRect.left - siblingULRect.width - outterUlWidth + 150 < 0 &&
                            outterUlWidth < window.innerWidth &&
                            outterUlWidth + siblingULRect.width + siblingULRect.width < window.innerWidth;
                    } else {
                        result = outterUlWidth + siblingULRect.right + siblingULRect.width + 50 > window.innerWidth &&
                            siblingULRect.right >= 0 &&
                            outterUlWidth + siblingULRect.width + siblingULRect.width < window.innerWidth;
                    }

                    // Cache the result
                    this.directionChangeMemo.set(cacheKey, result);
                    return result;
                }
            } catch (error) {
                console.error("Error in directionChange:", error);
            }
            return false;
        },

        // Group direction change - optimized
        groupDirChange: function () {
            let elemList = {
                added: [],
                removed: [],
                clearNavDropdown: false,
            };

            try {
                if (document.querySelector("html").getAttribute("data-nav-layout") === "horizontal" && window.innerWidth > 992) {
                    let activeMenus = document.querySelectorAll(".slide.has-sub.open > ul");

                    // Process active menus
                    activeMenus.forEach((e) => {
                        let target = e;
                        let html = document.documentElement;
                        const listItem = target.closest("li");

                        if (!listItem) return;

                        // Get the position of the clicked element
                        var dropdownRect = listItem.getBoundingClientRect();
                        var dropdownWidth = target.getBoundingClientRect().width;

                        // Calculate the right edge position
                        var rightEdge = dropdownRect.right + dropdownWidth;
                        var leftEdge = dropdownRect.left - dropdownWidth;

                        if (html.getAttribute("dir") == "rtl") {
                            // Handle RTL direction
                            this._handleRtlDirection(e, dropdownRect, leftEdge, rightEdge, elemList, target, listItem);
                        } else {
                            // Handle LTR direction
                            this._handleLtrDirection(e, dropdownRect, leftEdge, rightEdge, elemList, target, listItem);
                        }
                    });

                    // Handle forced left items
                    this._handleForcedLeftItems(elemList);

                    // Handle visible elements
                    this._handleVisibleElements(elemList);
                }

                // Remove duplicates
                elemList.added = [...new Set(elemList.added)];
                elemList.removed = [...new Set(elemList.removed)];
            } catch (error) {
                console.error("Error in groupDirChange:", error);
            }

            return elemList;
        },

        // Helper methods for groupDirChange
        _handleRtlDirection: function (e, dropdownRect, leftEdge, rightEdge, elemList, target, listItem) {
            if (e.classList.contains("child1")) {
                if (dropdownRect.left < 0) {
                    elemList.clearNavDropdown = true;
                }
            }

            if (leftEdge < 0) {
                elemList.added.push(target.previousElementSibling.getAttribute("data-id"));
            } else {
                if (listItem.closest("ul").classList.contains("force-left") && rightEdge < window.innerWidth) {
                    elemList.added.push(target.previousElementSibling.getAttribute("data-id"));
                } else {
                    // Reset classes and position if not moving out
                    elemList.removed.push(target.previousElementSibling.getAttribute("data-id"));
                }
            }
        },

        _handleLtrDirection: function (e, dropdownRect, leftEdge, rightEdge, elemList, target, listItem) {
            if (e.classList.contains("child1")) {
                if (dropdownRect.right > window.innerWidth) {
                    elemList.clearNavDropdown = true;
                }
            }

            if (rightEdge > window.innerWidth) {
                elemList.added.push(target.previousElementSibling.getAttribute("data-id"));
            } else {
                if (listItem.closest("ul").classList.contains("force-left") && leftEdge > 0) {
                    elemList.added.push(target.previousElementSibling.getAttribute("data-id"));
                } else if (leftEdge < 0) {
                    elemList.removed.push(target.previousElementSibling.getAttribute("data-id"));
                } else {
                    elemList.removed.push(target.previousElementSibling.getAttribute("data-id"));
                }
            }
        },

        _handleForcedLeftItems: function (elemList) {
            let leftForceItem = document.querySelector(".slide-menu.active.force-left");
            if (leftForceItem) {
                if (document.querySelector("html").getAttribute("dir") != "rtl") {
                    let check = leftForceItem.getBoundingClientRect().right;
                    if (check < innerWidth) {
                        elemList.removed.push(leftForceItem.previousElementSibling.getAttribute("data-id"));
                    } else if (leftForceItem.getBoundingClientRect().left < 0) {
                        if (document.documentElement.getAttribute("data-nav-style") == "menu-hover" ||
                            document.documentElement.getAttribute("data-nav-style") == "icon-hover" ||
                            window.innerWidth > 992) {
                            elemList.removed.push(leftForceItem.previousElementSibling.getAttribute("data-id"));
                        }
                    }
                } else {
                    let check = leftForceItem.getBoundingClientRect().left -
                        leftForceItem.parentElement.closest(".slide-menu")?.clientWidth -
                        leftForceItem.getBoundingClientRect().width;
                    if (check > 0) {
                        if (document.documentElement.getAttribute("data-nav-style") == "menu-hover" ||
                            document.documentElement.getAttribute("data-nav-style") == "icon-hover" ||
                            window.innerWidth > 992) {
                            elemList.removed.push(leftForceItem.previousElementSibling.getAttribute("data-id"));
                        }
                    }
                }
            }
        },

        _handleVisibleElements: function (elemList) {
            let elements = document.querySelectorAll(".main-menu .has-sub ul");
            elements.forEach((e) => {
                if (isElementVisible(e)) {
                    let ele = e.getBoundingClientRect();
                    if (document.documentElement.getAttribute("dir") == "rtl") {
                        if (ele.left < 0) {
                            if (e.classList.contains("child1")) {
                                elemList.removed.push(e.previousElementSibling.getAttribute("data-id"));
                            } else {
                                elemList.added.push(e.previousElementSibling.getAttribute("data-id"));
                            }
                        }
                    } else {
                        if (ele.right > innerWidth) {
                            if (e.classList.contains("child1")) {
                                elemList.removed.push(e.previousElementSibling.getAttribute("data-id"));
                            } else {
                                elemList.added.push(e.previousElementSibling.getAttribute("data-id"));
                            }
                        }
                    }
                }
            });
        },

        // Scroll handling - optimized
        updateScrollVisibility: function (dotnetHelper) {
            // Debounced scroll handler for better performance
            let scrollTimeout;
            window.onscroll = function () {
                if (scrollTimeout) {
                    clearTimeout(scrollTimeout);
                }

                scrollTimeout = setTimeout(() => {
                    var scrollHeight = window.scrollY;
                    dotnetHelper.invokeMethodAsync('UpdateScrollVisibility', scrollHeight);
                }, 10); // Small debounce for better performance
            };
        },

        scrollToTop: function () {
            window.scrollTo({
                top: 0,
                behavior: "smooth"
            });
        },

        registerScrollListener: function (dotnetHelper) {
            // Optimized scroll event with throttling
            let lastScrollY = 0;
            let ticking = false;

            const scrollHandler = function () {
                lastScrollY = window.scrollY || window.pageYOffset;

                if (!ticking) {
                    window.requestAnimationFrame(function () {
                        dotnetHelper.invokeMethodAsync("SetStickyClass", lastScrollY);
                        ticking = false;
                    });

                    ticking = true;
                }
            };

            window.addEventListener('scroll', scrollHandler, { passive: true });

            // Trigger initial check
            var scrollY = window.scrollY || window.pageYOffset;
            dotnetHelper.invokeMethodAsync("SetStickyClass", scrollY);
        },

        // Initialize Bootstrap components
        initializeTooltips: function () {
            try {
                const tooltipTriggerList = document.querySelectorAll('[data-bs-toggle="tooltip"]');
                const tooltipList = [...tooltipTriggerList].map((tooltipTriggerEl) => new bootstrap.Tooltip(tooltipTriggerEl, {
                    boundary: document.body // Improve performance by setting boundary
                }));
            } catch (error) {
                console.error("Error initializing tooltips:", error);
            }
        },

        initializePopover: function () {
            try {
                const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]');
                const popoverList = [...popoverTriggerList].map((popoverTriggerEl) => new bootstrap.Popover(popoverTriggerEl, {
                    boundary: document.body // Improve performance by setting boundary
                }));
            } catch (error) {
                console.error("Error initializing popovers:", error);
            }
        },

        // Card functions - optimized
        initCardRemove: function () {
            try {
                let DIV_CARD = ".card";
                let cardRemoveBtn = document.querySelectorAll('[data-bs-toggle="card-remove"]');
                cardRemoveBtn.forEach((ele) => {
                    ele.addEventListener("click", function (e) {
                        e.preventDefault();
                        let card = this.closest(DIV_CARD);
                        if (card) {
                            // Add fade-out animation before removal
                            card.style.transition = "opacity 0.3s";
                            card.style.opacity = "0";
                            setTimeout(() => {
                                card.remove();
                            }, 300);
                        }
                        return false;
                    });
                });
            } catch (error) {
                console.error("Error in initCardRemove:", error);
            }
        },

        initCardFullscreen: function () {
            try {
                let DIV_CARD = ".card";
                let cardFullscreenBtn = document.querySelectorAll('[data-bs-toggle="card-fullscreen"]');
                cardFullscreenBtn.forEach((ele) => {
                    ele.addEventListener("click", function (e) {
                        let card = this.closest(DIV_CARD);
                        if (card) {
                            card.classList.toggle("card-fullscreen");
                            card.classList.remove("card-collapsed");
                        }
                        e.preventDefault();
                        return false;
                    });
                });
            } catch (error) {
                console.error("Error in initCardFullscreen:", error);
            }
        }
    };
})();

// Helper function to check element visibility
function isElementVisible(element) {
    try {
        if (!element) return false;
        const computedStyle = window.getComputedStyle(element);
        return computedStyle.display !== "none" && computedStyle.visibility !== "hidden";
    } catch (error) {
        console.error("Error in isElementVisible:", error);
        return false;
    }
}

// Performance optimization: Use passive event listeners where possible
document.addEventListener('DOMContentLoaded', function () {
    window.addEventListener('scroll', function () { }, { passive: true });
    window.addEventListener('resize', function () { }, { passive: true });
});