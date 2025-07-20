function renderChart(canvasId, labels, data, label) {
    const canvas = document.getElementById(canvasId);
    const ctx = canvas.getContext('2d');
    if (window.myCharts && window.myCharts[canvasId]) {
        window.myCharts[canvasId].destroy();
    }
    window.myCharts = window.myCharts || {};
    window.myCharts[canvasId] = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: label,
                data: data,
                borderColor: 'rgb(75, 192, 192)',
                tension: 0.1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
} 