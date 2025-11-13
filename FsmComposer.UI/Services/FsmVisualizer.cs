using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using FsmComposer.Core.Models;

namespace FsmComposer.UI.Services
{
    public static class FsmVisualizer
    {
        public static void DrawFsm(Canvas canvas, FiniteStateMachine fsm, double startX = 100, double startY = 100)
        {
            canvas.Children.Clear();

            var statePositions = new Dictionary<string, Point>();
            double radius = 30;
            double spacingX = 120;
            double spacingY = 100;

            // 1. Расположим состояния в сетке
            int col = 0, row = 0;
            foreach (var state in fsm.States)
            {
                double x = startX + col * spacingX;
                double y = startY + row * spacingY;

                // Круг
                var ellipse = new Ellipse
                {
                    Width = radius * 2,
                    Height = radius * 2,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                    Fill = fsm.AcceptStates.Contains(state) ? Brushes.LightGreen : Brushes.LightBlue
                };
                Canvas.SetLeft(ellipse, x - radius);
                Canvas.SetTop(ellipse, y - radius);
                canvas.Children.Add(ellipse);

                // Подпись
                var label = new TextBlock
                {
                    Text = state,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Canvas.SetLeft(label, x - 15);
                Canvas.SetTop(label, y - 15);
                canvas.Children.Add(label);

                statePositions[state] = new Point(x, y);

                col++;
                if (col > 3) { col = 0; row++; }
            }

            // 2. Начальное состояние — стрелка
            if (fsm.States.Contains(fsm.StartState))
            {
                var start = statePositions[fsm.StartState];
                var arrow = new Polygon
                {
                    Points = new PointCollection
                    {
                        new Point(start.X - 60, start.Y),
                        new Point(start.X - 40, start.Y - 10),
                        new Point(start.X - 40, start.Y + 10)
                    },
                    Fill = Brushes.Black
                };
                canvas.Children.Add(arrow);
            }

            // 3. Переходы
            foreach (var kvp in fsm.Transitions)
            {
                var (from, symbol) = kvp.Key;
                var tos = kvp.Value;

                if (!statePositions.ContainsKey(from)) continue;

                foreach (var to in tos)
                {
                    if (!statePositions.ContainsKey(to)) continue;

                    var fromPos = statePositions[from];
                    var toPos = statePositions[to];

                    // Линия
                    var line = new Line
                    {
                        X1 = fromPos.X,
                        Y1 = fromPos.Y,
                        X2 = toPos.X,
                        Y2 = toPos.Y,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1.5
                    };
                    canvas.Children.Add(line);

                    // Стрелка
                    var arrowHead = CreateArrowHead(fromPos, toPos);
                    canvas.Children.Add(arrowHead);

                    // Символ
                    var mid = new Point((fromPos.X + toPos.X) / 2, (fromPos.Y + toPos.Y) / 2);
                    var symbolLabel = new TextBlock
                    {
                        Text = symbol?.ToString() ?? "ε",
                        FontSize = 12,
                        Foreground = Brushes.DarkRed
                    };
                    Canvas.SetLeft(symbolLabel, mid.X - 10);
                    Canvas.SetTop(symbolLabel, mid.Y - 20);
                    canvas.Children.Add(symbolLabel);
                }
            }
        }

        private static Polygon CreateArrowHead(Point from, Point to)
        {
            double angle = System.Math.Atan2(to.Y - from.Y, to.X - from.X);
            double arrowLength = 15;
            double arrowAngle = System.Math.PI / 6;

            var p1 = new Point(
                to.X - arrowLength * System.Math.Cos(angle - arrowAngle),
                to.Y - arrowLength * System.Math.Sin(angle - arrowAngle)
            );
            var p2 = new Point(
                to.X - arrowLength * System.Math.Cos(angle + arrowAngle),
                to.Y - arrowLength * System.Math.Sin(angle + arrowAngle)
            );

            return new Polygon
            {
                Points = new PointCollection { to, p1, p2 },
                Fill = Brushes.Black
            };
        }
    }
}