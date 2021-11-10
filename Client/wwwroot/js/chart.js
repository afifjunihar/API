$(document).ready(function () {
    chart.render();
});

const dataSeries = [];
const dataLabel = [];

var dataProp = $.ajax({
  type: 'GET',
  url: 'https://localhost:5001/api/employees/chart',
  success: function (data) {
    $.each(data, function (index, value) {
      dataSeries.push(value.count);
      dataLabel.push(value.degree);
		})
	}
})

// ApexChart
var options = {
  chart: {
    type: 'pie'
  },
  series: dataSeries,
  labels: dataLabel
}

var chart = new ApexCharts(document.querySelector("#chart"), options);
