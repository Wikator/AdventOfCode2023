package day18

import scala.annotation.tailrec
import scala.io.Source

case class Coords(Y: Int, X: Int)

def inputLines: Array[String] = {
  val bufferedSource = Source.fromFile("src/data/input.txt")
  val lines = bufferedSource.getLines.toArray
  bufferedSource.close
  lines
}

@tailrec
def buildTrench(lines: Array[String], acc: List[String] = List("."), currentCoords: Coords = Coords(0, 0)): List[String] = {
  if (lines.isEmpty)
    acc
  else
    val splitLine = lines.head.split(" ")
    val newAcc = splitLine(0) match
      case "R" =>
        val temp = if (acc.head.length - 1 < currentCoords.X + splitLine(1).toInt)
          acc.map(line => line.concat("." * (currentCoords.X + splitLine(1).toInt - acc.head.length + 1)))
        else
          acc
        temp.zipWithIndex.map((line, index) => {
          if (currentCoords.Y != index)
            line
          else
            line.zipWithIndex.map((char, index2) => {
              if (currentCoords.X < index2 && currentCoords.X + splitLine(1).toInt >= index2)
                '#'
              else
                char
            }).foldLeft("")((acc, curr) => acc + curr)
        })
      case "L" =>
        val temp = if (0 > currentCoords.X - splitLine(1).toInt)
          acc.map(line => ("." * Math.abs(currentCoords.X - splitLine(1).toInt)).concat(line))
        else
          acc
        val newCurrentCords = if (0 > currentCoords.X - splitLine(1).toInt)
          Coords(currentCoords.Y, Math.abs(currentCoords.X - splitLine(1).toInt) + currentCoords.X)
        else
          currentCoords
        temp.zipWithIndex.map((line, index) => {
          if (newCurrentCords.Y != index)
            line
          else
            line.zipWithIndex.map((char, index2) => {
              if (newCurrentCords.X > index2 && newCurrentCords.X - splitLine(1).toInt <= index2)
                '#'
              else
                char
            }).foldLeft("")((acc, curr) => acc + curr)
        })
      case "U" =>
        val temp = if (currentCoords.Y - splitLine(1).toInt < 0)
          List.fill(Math.abs(currentCoords.Y - splitLine(1).toInt))("." * acc.head.length).concat(acc)
        else
          acc
        val newCurrentCords = if (currentCoords.Y - splitLine(1).toInt < 0)
          Coords(Math.abs(currentCoords.Y - splitLine(1).toInt) + currentCoords.Y, currentCoords.X)
        else
          currentCoords
        temp.zipWithIndex.map((line, index) => {
          if (newCurrentCords.Y <= index || currentCoords.Y - splitLine(1).toInt > index)
            line
          else
            line.zipWithIndex.map((char, index2) => {
              if (newCurrentCords.X == index2)
                '#'
              else
                char
            }).foldLeft("")((acc, curr) => acc + curr)
        })
      case "D" =>
        val temp = if (acc.length - 1 < currentCoords.Y + splitLine(1).toInt)
          acc.concat(List.fill(currentCoords.Y + splitLine(1).toInt - acc.length + 1)("." * acc.head.length))
        else
          acc
        temp.zipWithIndex.map((line, index) => {
          if (currentCoords.Y >= index || currentCoords.Y + splitLine(1).toInt + 1 <= index)
            line
          else
            line.zipWithIndex.map((char, index2) => {
              if (currentCoords.X == index2)
                '#'
              else
                char
            }).foldLeft("")((acc, curr) => acc + curr)
        })

    val newCoords = splitLine(0) match
      case "R" => Coords(currentCoords.Y, currentCoords.X + splitLine(1).toInt)
      case "L" => Coords(currentCoords.Y, if (0 > currentCoords.X - splitLine(1).toInt) then 0 else currentCoords.X - splitLine(1).toInt)
      case "U" => Coords(if (currentCoords.Y - splitLine(1).toInt) < 0 then 0 else currentCoords.Y - splitLine(1).toInt, currentCoords.X)
      case "D" => Coords(currentCoords.Y + splitLine(1).toInt, currentCoords.X)
    buildTrench(lines.tail, newAcc, newCoords)
}


@tailrec
def floodFill(trenches: List[String], startingPositions: List[Coords]): List[String] = {
  if (startingPositions.isEmpty)
    trenches
  else
    val newStartingPositions = startingPositions.foldLeft(List[Coords]())((acc, curr) => {
      val possible: List[Coords] = List(Coords(curr.Y + 1, curr.X), Coords(curr.Y - 1, curr.X), Coords(curr.Y, curr.X + 1), Coords(curr.Y, curr.X - 1))
      acc.concat(possible.foldLeft(List[Coords]())((acc1, curr1) => if trenches(curr1.Y)(curr1.X) == '.' && !acc.exists((c: Coords) => c.Y == curr1.Y && c.X == curr1.X) then curr1 :: acc1 else acc1))
    })
    val newTrenches = trenches.zipWithIndex.map((line, y) => line.zipWithIndex.map((char, x) => if newStartingPositions.exists((c: Coords) => c.Y == y && c.X == x) then '$' else char).foldLeft("")((acc, curr) => acc + curr))
    floodFill(newTrenches, newStartingPositions)
}


@main
def Main1(): Unit = {
  val lines = inputLines
  val trenches = buildTrench(lines)
  trenches.foreach(line => println(line))
  val filledGrid = floodFill(trenches, List[Coords](Coords(160, 163)))
  filledGrid.foreach(println)
  println(filledGrid.foldLeft(0)((acc1, curr1) => acc1 + curr1.foldLeft(0)((acc2, curr2) => if curr2 != '.' then acc2 + 1 else acc2)))
}