var jsFunctions = {};

jsFunctions.calculateSquareRoot = function () {
	const number = prompt("Enter your number");

	DotNet.invokeMethodAsync("BlazorProducts.Client", "CalculateSquareRoot",
		parseInt(number))
		.then(result => {
			var el = document.getElementById("string-result");
			el.innerHTML = result;
		});
}

jsFunctions.calculateSquareRootJustResult = function () {
	const number = prompt("Enter your number");

	DotNet.invokeMethodAsync("BlazorProducts.Client", "CalculateSquareRootJustResult",
		parseInt(number), true)
		.then(result => {
			var el = document.getElementById("result");
			el.innerHTML = result;
		});
}

jsFunctions.registerMouseCoordinatesHandler = function (dotNetObjRef) {
	function mouseCoordinatesHandler() {
		dotNetObjRef.invokeMethodAsync("ShowCoordinates",
			{
				x: window.event.screenX,
				y: window.event.screenY
			});
	};

	mouseCoordinatesHandler();

	document.getElementById("coordinates").onmousemove = mouseCoordinatesHandler;
}