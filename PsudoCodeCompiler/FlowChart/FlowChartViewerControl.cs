using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PseudoCodeCompiler.Runtime;
using PseudoCodeCompiler.Runtime.Instructions;

namespace PseudoCodeCompiler.FlowChart
{
    public partial class FlowChartViewerControl : UserControl
    {
        PsudoProgram program = null;

        public FlowChartViewerControl()
        {
            InitializeComponent();
        }

        public void ShowProgram(PsudoProgram prog)
        {
            this.program = prog;
            this.drawingPanel.Roots.Clear();

            this.drawingPanel.Controls.Clear();
            FlowChartItemControl item = new FlowChartItemControl(program.MainMethod.Name);
            item.IsStartEndProgram = true;

            this.drawingPanel.Controls.Add(item);
            CreateChildren(program.MainMethod.Instructions, item);
            
            item.Location = new Point(500, 10);

            Rectangle treeBounds = item.GetTreeBounds();
            int newLeft = item.Left + treeBounds.Left * -1 + 20;
            int newTop = item.Top + treeBounds.Top * -1 + 20;
            item.Location = new Point(newLeft, newTop);

            this.drawingPanel.Roots.Add(item);
            this.drawingPanel.UpdateTreeBounds();

            this.Invalidate();
            item.DisableAutoPlacement();
        }

        private void CreateChildren(List<PsudoInstruction> instructions, FlowChartItemControl parent)
        {
            FlowChartItemControl tempParent = parent;
            FlowChartItemControl item = null;
            foreach (PsudoInstruction inst in instructions)
            {
                item = new FlowChartItemControl(inst);
                this.drawingPanel.Controls.Add(item);

                if (inst is BlockInstruction)
                {
                    tempParent.AddChild(item);
                    CreateChildren(((BlockInstruction)inst).Instructions, item);
                }
                else if (inst is DecisionInstruction)
                {
                    tempParent.AddChild(item);
                    DecisionInstruction decision = (DecisionInstruction)inst;

                    if (decision.FalseInstruction is BlockInstruction || decision.FalseInstruction is NoOpInstruction)
                    {
                        FlowChartItemControl trueBlock = new FlowChartItemControl("TRUE");
                        FlowChartItemControl falseBlock = new FlowChartItemControl("FALSE");
                        trueBlock.IsLabelNode = true; 
                        falseBlock.IsLabelNode = true;

                        item.AddChild(trueBlock);
                        item.AddChild(falseBlock);

                        this.drawingPanel.Controls.Add(trueBlock);
                        this.drawingPanel.Controls.Add(falseBlock);

                        CreateChildren(decision.CodeBlock.Instructions, trueBlock);
                        if (decision.FalseInstruction is BlockInstruction)
                        {
                            CreateChildren(((BlockInstruction)decision.FalseInstruction).Instructions, falseBlock);
                        }
                    }
                    else
                    {
                        CreateChildren(decision.CodeBlock.Instructions, item);
                        while (decision.FalseInstruction is DecisionInstruction)
                        {
                            decision = (DecisionInstruction)decision.FalseInstruction;

                            CreateChildren(decision.CodeBlock.Instructions, item);
                        }
                    }

                    FlowChartItemControl end = new FlowChartItemControl("End");
                    this.drawingPanel.Controls.Add(end);

                    foreach (FlowChartItemControl child in item.Children)
                        AddToEndofChildren(child, end);

                    item = end;
                }
                else if (inst is DoLoopInstruction)
                {
                    DoLoopInstruction loop = (DoLoopInstruction)inst;
                    FlowChartItemControl doNode = new FlowChartItemControl("DO");
                    this.drawingPanel.Controls.Add(doNode);
                    tempParent.AddChild(doNode);

                    CreateChildren(loop.Instructions, doNode);
                    AddToEndofChildren(tempParent, item);
                    item.LoopBack = doNode;
                }
                else if (inst is ForLoopInstruction)
                {
                    ForLoopInstruction loop = (ForLoopInstruction)inst;
                    FlowChartItemControl initNode = new FlowChartItemControl(loop.InitInstruction);
                    this.drawingPanel.Controls.Add(initNode);
                    tempParent.AddChild(initNode);

                    initNode.AddChild(item);
                    CreateChildren(loop.Instructions, item);

                    FlowChartItemControl updateNode = new FlowChartItemControl(loop.UpdateInstruction);
                    this.drawingPanel.Controls.Add(updateNode);
                    AddToEndofChildren(item, updateNode);
                    updateNode.LoopBack = item;
                }
                else if (inst is WhileLoopInstruction)
                {
                    tempParent.AddChild(item);

                    WhileLoopInstruction loop = (WhileLoopInstruction)inst;
                    CreateChildren(loop.Instructions, item);
                    FlowChartItemControl temp = item.Children[0];

                    while (temp.Children.Count > 0)
                    {
                        temp = temp.Children[0];
                    }
                    temp.LoopBack = item;
                }
                else
                {
                    tempParent.AddChild(item);
                }
                tempParent = item;
            }
        }

        private void AddToEndofChildren(FlowChartItemControl parent, FlowChartItemControl child)
        {
            while (parent.Children.Count > 0)
            {
                parent = parent.Children[0];
            }

            parent.AddChild(child);
        }
    }
}
