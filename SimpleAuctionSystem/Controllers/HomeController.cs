using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SimpleAuctionSystem.Context;
using SimpleAuctionSystem.Models;
using SimpleAuctionSystem.ViewModels;

namespace SimpleAuctionSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;


        public HomeController()
        {
            _context = new ApplicationContext();
        }

        // GET: Home
        public ActionResult Index()
        {
            var itemModel = new ItemViewModel();

            return View(itemModel);
        }

        #region New Item

        public ActionResult GetAllItems()
        {
            var Items = _context.Items.ToList();
            var itemsList = Items.Select(i => new ItemViewModel
            {
                ItemId = i.Id,
                ItemName = i.ItemName,
                ItemStartPrice = i.StartPrice
            });

            return PartialView("_PartialItemsTable", itemsList);
        }

        public ActionResult AddNewItem(ItemViewModel model)
        {
            Response response = new Response();
            if (ModelState.IsValid)
            {
                try
                {
                    if(model.ItemId == 0)
                    {
                        var NewItem = new Items
                        {
                            ItemName=model.ItemName,
                            StartPrice = model.ItemStartPrice
                        };

                        _context.Items.Add(NewItem);
                        _context.SaveChanges();

                        response.Success = true;
                        response.Message = "Saved Successfully";
                    }
                }catch(Exception e)
                {
                    response.Success = false;
                    response.Message += "Error => " +  e.Message;
                    return Json(response);
                }
            }
            else
            {
                foreach (var modelStateKey in ViewData.ModelState.Keys)
                {
                    var modelStateVal = ViewData.ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        var key = modelStateKey;
                        var errorMessage = error.ErrorMessage;
                        response.Message += key + " => " + error.ErrorMessage + "\n";
                    }
                }

                return Json(response);
            }
            return Json(response);
        }

        #endregion


        #region ParticipantRegion

        public ActionResult GetParticipantForm()
        {
            var participantModel = new ParticipantViewModel();

            return PartialView("_ParticipantForm", participantModel);
        }

        public ActionResult GetAllParticipants()
        {
            var Paricipants = _context.Participants.ToList();
            var participantsList = Paricipants.Select(p => new ParticipantViewModel
            {
                participantId =p.Id ,
                ParticipantName = p.ParticipantName
            });

            return PartialView("_ParticipantsTable", participantsList);
        }

        public ActionResult AddNewParticipant(ParticipantViewModel model)
        {
            Response response = new Response();
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.participantId == 0)
                    {
                        var NewParticipant = new Participants
                        {
                            ParticipantName = model.ParticipantName,
                        };

                        _context.Participants.Add(NewParticipant);
                        _context.SaveChanges();

                        response.Success = true;
                        response.Message = "Saved Successfully";
                    }
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message += "Error => " + e.Message;
                    return Json(response);
                }
            }
            else
            {
                foreach (var modelStateKey in ViewData.ModelState.Keys)
                {
                    var modelStateVal = ViewData.ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        var key = modelStateKey;
                        var errorMessage = error.ErrorMessage;
                        response.Message += key + " => " + error.ErrorMessage + "\n";
                    }
                }

                return Json(response);
            }
            return Json(response);
        }


        #endregion


        #region Auction Process

        public ActionResult GetAuctionForm()
        {
            var Auction = new AuctionViewModel
            {
                items = _context.Items.Select(i => new ItemViewModel
                {
                    ItemId = i.Id,
                    ItemName = i.ItemName
                }).ToList(),
                participants = _context.Participants.Select(p => new ParticipantViewModel
                {
                    participantId = p.Id,
                    ParticipantName = p.ParticipantName
                }).ToList()
            };
            return PartialView("_AuctionViewPartial", Auction);
        }


        public ActionResult GetAllAuctionProcess(decimal? itemId)
        {
            var Auctions = _context.AuctionProcesses.Include("Participants").Where(i => i.ItemId == itemId).ToList();
            var participantsList = Auctions.Select(p => new AuctionViewModel
            {
                ParticipantName = p.Participants.Where( i => i.Id == p.ParticipantId).SingleOrDefault().ParticipantName,
                Bid = p.Bid,
                Profit = p.Profit,                
            });

            return PartialView("_AuctionPartialTable", participantsList);
        }

        [HttpPost]
        public ActionResult GetItemStartPrice(decimal? itemId)
        {
            try
            {
                var itemprice = _context.Items.SingleOrDefault(i => i.Id == itemId).StartPrice;
                return Json(new { price = itemprice }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new {Message = ex.Message }, JsonRequestBehavior.AllowGet );
            }
        }

        public ActionResult AddNewAuction(AuctionViewModel model)
        {
            Response response = new Response();
            if (ModelState.IsValid)
            {
                try
                {
                    var auction = _context.AuctionProcesses.Where(a => a.ItemId == model.ItemId && a.Bid < model.Bid).ToList();

                    AuctionProcess NewAuction = null ;
                    var item = _context.Items.SingleOrDefault(i => i.Id == model.ItemId);
                    if (auction != null && auction.Count != 0)
                    {
                        var auc = auction.LastOrDefault();
                        NewAuction = new AuctionProcess
                        {
                            ItemId = (int)model.ItemId,
                            ParticipantId = (int)model.ParticipantId,
                            Bid = model.Bid,
                            Profit = (model.Bid - item.StartPrice) - auc.Profit,
                            isWinner = false,
                        };
                    }
                    else
                    {

                        NewAuction = new AuctionProcess
                        {
                            ItemId = (int)model.ItemId,
                            ParticipantId = (int)model.ParticipantId,
                            Bid = model.Bid,
                            Profit = model.Bid - item.StartPrice,
                            isWinner = false,
                        };
                    }
                    
                    _context.AuctionProcesses.Add(NewAuction);
                    _context.SaveChanges();

                    response.Success = true;
                    response.Message = "Saved Successfully";
                    
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message += "Error => " + e.Message;
                    return Json(response);
                }
            }
            else
            {
                foreach (var modelStateKey in ViewData.ModelState.Keys)
                {
                    var modelStateVal = ViewData.ModelState[modelStateKey];
                    foreach (var error in modelStateVal.Errors)
                    {
                        var key = modelStateKey;
                        var errorMessage = error.ErrorMessage;
                        response.Message += key + " => " + error.ErrorMessage + "\n";
                    }
                }

                return Json(response);
            }
            return Json(response);
        }

        public ActionResult GetLastPrice(decimal? Bid , decimal? itemId)
        {
            Response response = new Response();
            try
            {
                var auction = _context.AuctionProcesses.Where(a => a.Bid < Bid && a.Item.StartPrice < Bid && a.ItemId == itemId).SingleOrDefault();

                if(auction == null)
                {
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                }
            }catch(Exception e)
            {
                response.Success = false;
                response.Message = "Error";
            }
            return Json(response , JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}