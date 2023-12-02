package day2

case class MinValues(minBlue: Int, minGreen: Int, minRed: Int)

def minValue(array: Option[Array[Cube]]): Int = {
  val extractedValue = array match
    case Some(value) => value
    case None => throw Exception()

  extractedValue.foldLeft(0)((acc, curr) => if curr.quantity > acc then curr.quantity else acc)
}

@main
def Main2(): Unit = {
  val minValues = games().map(g => {
    val cubes = g.cubes.flatten.groupBy(c => c.color)
    val blueMinValue = minValue(cubes.get('b'))
    val greenMinValue = minValue(cubes.get('g'))
    val redMinValue = minValue(cubes.get('r'))

    MinValues(blueMinValue, greenMinValue, redMinValue)
  })

  val sum = minValues.foldLeft(0)((acc, curr) => acc + curr.minBlue * curr.minGreen * curr.minRed)
  println(sum)
}
