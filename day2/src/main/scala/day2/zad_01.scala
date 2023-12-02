package day2

case class Result(gameNumber: Int, isPossible: Boolean)

@main
def Main1(): Unit = {
  val results = games().map (g => {
    val isPossible = g.cubes.foldLeft(true) ((acc, curr) =>
      if acc then
        val cubes = curr.groupBy(c => c.color)
        cubes.foldLeft(true)((acc, curr) =>
          if acc then
            curr._1 match
              case 'b' => curr._2.foldLeft(0)((acc, curr) => acc + curr.quantity) <= 14
              case 'g' => curr._2.foldLeft(0)((acc, curr) => acc + curr.quantity) <= 13
              case 'r' => curr._2.foldLeft(0)((acc, curr) => acc + curr.quantity) <= 12
              case _ => throw Exception()
          else
            acc
        )
      else
        acc
    )
    Result (g.gameNumber, isPossible)
  })

  val sum = results.foldLeft(0)((acc, curr) => if curr.isPossible then acc + curr.gameNumber else acc)
  println(sum)
}
