// SignalR Client Configuration
window.signalRConfig = {
    configureSignalR: function () {
        // Configure SignalR connection options
        if (window.Blazor) {
            // Configure reconnection options
            window.Blazor.defaultReconnectionHandler._reconnectionDisplay = {
                show: function () {
                    console.log('Blazor reconnection started');
                },
                hide: function () {
                    console.log('Blazor reconnection completed');
                },
                failed: function () {
                    console.log('Blazor reconnection failed');
                }
            };

            // Set reconnection options
            window.Blazor.defaultReconnectionHandler._reconnectionOptions = {
                maxRetries: 5,
                retryIntervalMilliseconds: 2000,
                dialogId: 'components-reconnect-modal'
            };
        }

        // Configure SignalR transport options if available
        if (window.signalR) {
            window.signalR.HttpTransportType = {
                WebSockets: 1,
                ServerSentEvents: 2,
                LongPolling: 4
            };
        }
    },

    // Function to check connection status
    checkConnectionStatus: function () {
        if (window.Blazor && window.Blazor.defaultReconnectionHandler) {
            const connection = window.Blazor.defaultReconnectionHandler._connection;
            if (connection) {
                console.log('SignalR Connection State:', connection.state);
                console.log('Transport Type:', connection.transport?.name || 'Unknown');
                return connection.state;
            }
        }
        return 'Unknown';
    },

    // Function to force reconnection
    forceReconnect: function () {
        if (window.Blazor && window.Blazor.defaultReconnectionHandler) {
            window.Blazor.defaultReconnectionHandler._connection?.stop();
            setTimeout(() => {
                window.Blazor.defaultReconnectionHandler._connection?.start();
            }, 1000);
        }
    }
};

// Auto-configure when the page loads
document.addEventListener('DOMContentLoaded', function () {
    window.signalRConfig.configureSignalR();
    
    // Log connection status every 30 seconds for debugging
    setInterval(() => {
        const status = window.signalRConfig.checkConnectionStatus();
        if (status !== 'Connected') {
            console.log('Current connection status:', status);
        }
    }, 30000);
});

// Listen for connection state changes
if (window.Blazor) {
    window.Blazor.defaultReconnectionHandler._connection?.on('stateChanged', (oldState, newState) => {
        console.log(`SignalR connection state changed from ${oldState} to ${newState}`);
    });
} 