package day2

case class Result(gameNumber: Int, isPossible: Boolean)

def isGamePossible(game: Game): Boolean =
  game.sets.forall(set => isSetPossible(set))

def isSetPossible(set: Array[Cube]): Boolean =
  val groupedByColor = set.groupBy(cube => cube.color)
  groupedByColor.forall {
    case ('b', colorSet) => quantitySum(colorSet) <= 14
    case ('g', colorSet) => quantitySum(colorSet) <= 13
    case ('r', colorSet) => quantitySum(colorSet) <= 12
    case (_, _) => throw new Exception("Unexpected color")
  }

def quantitySum(set: Array[Cube]): Int =
  set.map(cube => cube.quantity).sum

@main
def Main1(): Unit = {
  val results = games().map (game => Result (game.gameNumber, isGamePossible(game)))
  val sum = results.foldLeft(0)((acc, curr) => if curr.isPossible then acc + curr.gameNumber else acc)
  println(sum)
}
