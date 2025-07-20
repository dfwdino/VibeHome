window.KidsChore_SetupResize = function (dotNetObjRef) {
    function report() {
        dotNetObjRef.invokeMethodAsync('KidsChore_OnResize', window.innerWidth);
    }
    window.addEventListener('resize', report);
    report();
}; 