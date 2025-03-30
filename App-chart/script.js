const COLORS = ["#1f77b4", "#ff7f0e", "#2ca02c", "#d62728", "#9467bd"];

class ChartBuilder {
  constructor(svg, width, height) {
    this.svg = svg;
    this.width = width;
    this.height = height;
    this.data = [];
  }

  setData(dataArray) {
    this.data = dataArray;
    return this;
  }

  clear() {
    this.svg.selectAll("*").remove();
    return this;
  }

  buildScales() {
    this.x = d3
      .scaleLinear()
      .domain([0, d3.max(this.data)])
      .range([0, this.width]);
    this.y = d3
      .scaleBand()
      .domain(this.data.map((d, i) => i))
      .range([0, this.height])
      .padding(0.1);
    return this;
  }

  buildAxes() {
    this.svg.append("g").call(d3.axisLeft(this.y).tickFormat((i) => i + 1));
    this.svg
      .append("g")
      .attr("transform", `translate(0, ${this.height})`)
      .call(d3.axisBottom(this.x));
    return this;
  }

  buildBars() {
    this.svg
      .selectAll(".bar")
      .data(this.data)
      .enter()
      .append("rect")
      .attr("class", "bar")
      .attr("y", (d, i) => this.y(i))
      .attr("x", 0)
      .attr("height", this.y.bandwidth())
      .attr("width", (d) => this.x(d))
      .attr("fill", (d, i) => COLORS[i % COLORS.length]);

    this.svg
      .selectAll(".bar-label")
      .data(this.data)
      .enter()
      .append("text")
      .attr("class", "bar-label")
      .attr("x", (d) => this.x(d) - 5)
      .attr("y", (d, i) => this.y(i) + this.y.bandwidth() / 2)
      .attr("dy", ".35em")
      .attr("text-anchor", "end")
      .text((d) => d);
    return this;
  }

  build() {
    return this.svg;
  }
}

class BarChart {
  constructor(containerId) {
    this.container = d3.select(containerId);
    this.margin = { top: 20, right: 20, bottom: 30, left: 40 };
    this.width = 500 - this.margin.left - this.margin.right;
    this.height = 300 - this.margin.top - this.margin.bottom;
    this.svg = this.container
      .append("svg")
      .attr("width", this.width + this.margin.left + this.margin.right)
      .attr("height", this.height + this.margin.top + this.margin.bottom)
      .append("g")
      .attr("transform", `translate(${this.margin.left},${this.margin.top})`);
  }

  updateData(dataArray) {
    const builder = new ChartBuilder(this.svg, this.width, this.height);
    builder.clear().setData(dataArray).buildScales().buildBars().build();
  }
}

function parseInputData(inputValue) {
  if (!inputValue.trim()) {
    throw new Error("El campo de entrada está vacío.");
  }
  const parts = inputValue.split(",").map((s) => s.trim());
  const numbers = parts.map((s) => {
    const num = parseInt(s, 10);
    if (isNaN(num)) {
      throw new Error(`Valor inválido: "${s}". Ingrese solo números enteros.`);
    }
    return num;
  });
  return numbers;
}

const barChart = new BarChart("#chart");

document.getElementById("update-btn").addEventListener("click", () => {
  const input = document.getElementById("data-input").value;
  try {
    const data = parseInputData(input);
    barChart.updateData(data);
  } catch (error) {
    alert(error.message);
  }
});
