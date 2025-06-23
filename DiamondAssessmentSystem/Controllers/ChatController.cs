using DiamondAssessmentSystem.Application.Interfaces;
using DiamondAssessmentSystem.Infrastructure.Enums;
using DiamondAssessmentSystem.Infrastructure.IRepository;
using DiamondAssessmentSystem.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using DiamondAssessmentSystem.Application.DTO;
using DiamondAssessmentSystem.Application.Services;

namespace DiamondAssessmentSystem.Controllers
{

    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("start")]
        public async Task<ActionResult<ConversationDTO>> StartOrGet(int customerId)
        {
            var convo = await _chatService.StartOrGetConversationAsync(customerId);
            return Ok(convo);
        }

        [HttpPost("{conversationId}/assign")]
        public async Task<IActionResult> Assign(int conversationId, [FromQuery] int employeeId)
        {
            await _chatService.AssignEmployeeAsync(conversationId, employeeId);
            return NoContent();
        }

        [HttpPost("{conversationId}/send")]
        public async Task<IActionResult> SendMessage(int conversationId, [FromBody] CreateMessageDTO dto)
        {
            await _chatService.SendMessageAsync(conversationId, dto);
            return Ok();
        }

        [HttpGet("{conversationId}/messages")]
        public async Task<ActionResult<List<ChatLogDTO>>> GetMessages(int conversationId)
        {
            var messages = await _chatService.GetChatHistoryAsync(conversationId);
            return Ok(messages);
        }

        [HttpGet("unassigned")]
        public async Task<ActionResult<List<ConversationDTO>>> GetUnassignedConversations()
        {
            var conversations = await _chatService.GetUnassignedConversationsAsync();
            return Ok(conversations);
        }

    }

}
